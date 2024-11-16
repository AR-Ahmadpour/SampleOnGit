using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Application.Common.Interfaces.Services;
using SharedKernel;

namespace Accreditation.Application.ZirMehvars.Edit;

internal sealed class EditZirMehvarCommandHandler : ICommandHandler<EditZirMehvarCommand, Guid>
{
    private readonly IZirMehvarRepository _zirMehvarRepository;
    private readonly ICurrentUser _userContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public EditZirMehvarCommandHandler(IZirMehvarRepository zirMehvarRepository,
                                       ICurrentUser userContext,
                                       IDateTimeProvider dateTimeProvider,
                                       IUnitOfWork unitOfWork)
    {
        _zirMehvarRepository = zirMehvarRepository;
        _userContext = userContext;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(EditZirMehvarCommand command, CancellationToken cancellationToken)
    {
        var zirMehvar = await _zirMehvarRepository.FindAsync(command.GUID, cancellationToken);
        if (zirMehvar is null)
        {
            return Result.Failure<Guid>(ZirMehvarErrors.NotFound);
        }

        if (zirMehvar.IsDeleted)
        {
            return Result.Failure<Guid>(ZirMehvarErrors.NotActive);
        }

        if (!await _zirMehvarRepository.IsTitleUniqueAsync(command.GUID, zirMehvar.MehvarGUID, command.Title, cancellationToken))
        {
            return Result.Failure<Guid>(ZirMehvarErrors.TitleNotUnique);
        }

        zirMehvar.Edit(command.Title, _dateTimeProvider.Now, Guid.Parse(_userContext.UserId),
            command.SortOrder, command.WeightedCoefficient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return zirMehvar.GUID;
    }
}
