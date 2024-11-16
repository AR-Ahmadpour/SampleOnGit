using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Common.Interfaces.Services;
using SharedKernel;

namespace Accreditation.Application.Mehvars.Edit;

internal sealed class EditMehvarCommandHandler
    : ICommandHandler<EditMehvarCommand, Guid>
{
    private readonly IMehvarRepository _mehvarRepository;
    private readonly ICurrentUser _userContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public EditMehvarCommandHandler(IMehvarRepository mehvarRepository,
                                    ICurrentUser userContext,
                                    IDateTimeProvider dateTimeProvider,
                                    IUnitOfWork unitOfWork)
    {
        _mehvarRepository = mehvarRepository;
        _userContext = userContext;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(EditMehvarCommand command, CancellationToken cancellationToken)
    {
        var mehvar = await _mehvarRepository.FindAsync(command.GUID, cancellationToken);
        if (mehvar is null)
        {
            return Result.Failure<Guid>(MehvarErrors.NotFound);
        }

        if (mehvar.IsDeleted)
        {
            return Result.Failure<Guid>(MehvarErrors.NotActive);
        }

        if (!await _mehvarRepository.IsTitleUniqueAsync(command.GUID, mehvar.EtebarDorehGUID, command.Title, cancellationToken))
        {
            return Result.Failure<Guid>(MehvarErrors.TitleNotUnique);
        }

        mehvar.Edit(command.Title, _dateTimeProvider.Now, Guid.Parse(_userContext.UserId),
            command.SortOrder, command.WeightedCoefficient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return mehvar.GUID;
    }
}
