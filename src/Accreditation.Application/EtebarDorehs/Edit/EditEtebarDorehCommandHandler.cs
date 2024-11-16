using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Services;
using SharedKernel;

namespace Accreditation.Application.EtebarDorehs.Edit;

public sealed class EditEtebarDorehCommandHandler
    : ICommandHandler<EditEtebarDorehCommand, Guid>
{
    private readonly IEtebarDorehRepository _etebarDorehRepository;
    private readonly ICurrentUser _userContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public EditEtebarDorehCommandHandler(IEtebarDorehRepository etebarDorehRepository,
                                         ICurrentUser userContext,
                                         IDateTimeProvider dateTimeProvider,
                                         IUnitOfWork unitOfWork)
    {
        _etebarDorehRepository = etebarDorehRepository;
        _userContext = userContext;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(
        EditEtebarDorehCommand command,
        CancellationToken cancellationToken)
    {
        var etebarDoreh = await _etebarDorehRepository.FindAsync(command.GUID, cancellationToken);
        if (etebarDoreh is null)
        {
            return Result.Failure<Guid>(EtebarDorehErrors.NotFound);
        }

        if (etebarDoreh.IsDeleted)
        {
            return Result.Failure<Guid>(EtebarDorehErrors.NotActive);
        }

        if (!await _etebarDorehRepository.IsTitleUniqueAsync(command.GUID, command.OrgTypeGUID, command.Title, cancellationToken))
        {
            return Result.Failure<Guid>(EtebarDorehErrors.TitleNotUnique);
        }

        if (command.IsCurrent)
        {
            var etebarDorehCurrentFounded = await _etebarDorehRepository.FindCurrentEtebarDorehAsync(command.OrgTypeGUID, cancellationToken);
            if (etebarDorehCurrentFounded != null)
            {
                etebarDorehCurrentFounded.SetIsCurrentFalse();
            }
        }

        etebarDoreh.Edit
              (command.OrgTypeGUID, command.Title, _dateTimeProvider.Now, Guid.Parse(_userContext.UserId),
               command.StartDate, command.EndDate, command.IsCurrent, command.SortOrder);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return etebarDoreh.GUID;
    }
}
