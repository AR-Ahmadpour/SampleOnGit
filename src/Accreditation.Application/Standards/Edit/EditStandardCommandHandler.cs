using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Application.Common.Interfaces.Services;
using SharedKernel;

namespace Accreditation.Application.Standards.Edit;

internal sealed class EditStandardCommandHandler
    : ICommandHandler<EditStandardCommand, Guid>
{
    private readonly IStandardRepository _standardRepository;
    private readonly ICurrentUser _userContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public EditStandardCommandHandler(IStandardRepository standardRepository,
                                      ICurrentUser userContext,
                                      IDateTimeProvider dateTimeProvider,
                                      IUnitOfWork unitOfWork)
    {
        _standardRepository = standardRepository;
        _userContext = userContext;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(EditStandardCommand command, CancellationToken cancellationToken)
    {
        var standard = await _standardRepository.FindAsync(command.GUID, cancellationToken);
        if (standard is null)
        {
            return Result.Failure<Guid>(StandardErrors.NotFound);
        }

        if (standard.IsDeleted)
        {
            return Result.Failure<Guid>(StandardErrors.NotActive);
        }

        if (!await _standardRepository.IsTitleUniqueAsync(command.GUID, standard.ZirMehvarGUID, command.Title, cancellationToken))
        {
            return Result.Failure<Guid>(StandardErrors.TitleNotUnique);
        }

        if (!await _standardRepository.IsCodeUnique(command.GUID, standard.ZirMehvarGUID, command.Code, cancellationToken))
        {
            return Result.Failure<Guid>(StandardErrors.CodeNotUnique);
        }

        standard.Edit(command.Title, command.ShortTitle, command.Code, _dateTimeProvider.Now, Guid.Parse(_userContext.UserId),
            command.SortOrder, command.WeightedCoefficient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return standard.GUID;
    }
}
