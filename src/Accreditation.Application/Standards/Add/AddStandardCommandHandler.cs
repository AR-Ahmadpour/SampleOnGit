using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.ZirMehvars;
using Accreditation.Domain.Standards.Entities;
using SharedKernel;

namespace Accreditation.Application.Standards.Add;

internal sealed class AddStandardCommandHandler : ICommandHandler<AddStandardCommand, Guid>
{
    private readonly IStandardRepository _standardRepository;
    private readonly ICurrentUser _userContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IZirMehvarRepository _zirMehvarRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddStandardCommandHandler(IStandardRepository standardRepository,
                                     ICurrentUser userContext,
                                     IDateTimeProvider dateTimeProvider,
                                     IZirMehvarRepository zirMehvarRepository,
                                     IUnitOfWork unitOfWork)
    {
        _standardRepository = standardRepository;
        _userContext = userContext;
        _dateTimeProvider = dateTimeProvider;
        _zirMehvarRepository = zirMehvarRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(
        AddStandardCommand command,
        CancellationToken cancellationToken)
    {
        if (await _zirMehvarRepository.FindAsync(command.ZirMehvarGUID, cancellationToken) is null)
        {
            return Result.Failure<Guid>(ZirMehvarErrors.NotFound);
        }

        if (!await _standardRepository.IsTitleUniqueAsync(null, command.ZirMehvarGUID, command.Title, cancellationToken))
        {
            return Result.Failure<Guid>(StandardErrors.TitleNotUnique);
        }

        if (!await _standardRepository.IsCodeUnique(null, command.ZirMehvarGUID, command.Code, cancellationToken))
        {
            return Result.Failure<Guid>(StandardErrors.CodeNotUnique);
        }

        var standard = Standard.Create(
            command.ZirMehvarGUID,
            command.Title,
            command.ShortTitle,
            command.Code,
            command.SortOrder,
            Guid.Parse(_userContext.UserId),
            _dateTimeProvider.Now,
            command.WeightedCoefficient);

        _standardRepository.Add(standard);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return standard.GUID;
    }
}
