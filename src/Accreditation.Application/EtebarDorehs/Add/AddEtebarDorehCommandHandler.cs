using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Domain.EtebarDorehs.Entities;
using SharedKernel;

namespace Accreditation.Application.EtebarDorehs.Add;

public sealed class AddEtebarDorehCommandHandler : ICommandHandler<AddEtebarDorehCommand, Guid>
{
    private readonly IEtebarDorehRepository _etebarDorehRepository;
    private readonly ICurrentUser _userContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public AddEtebarDorehCommandHandler(IEtebarDorehRepository etebarDorehRepository,
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
        AddEtebarDorehCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _etebarDorehRepository.IsTitleUniqueAsync( null,command.OrgTypeGUID, command.Title, cancellationToken))
        {
            return Result.Failure<Guid>(EtebarDorehErrors.TitleNotUnique);
        }

        if (command.IsCurrent)
        {
            var etebarDorehCurrentFounded = await _etebarDorehRepository.FindCurrentEtebarDorehAsync(command.OrgTypeGUID,cancellationToken);
            if (etebarDorehCurrentFounded != null)
            {
                etebarDorehCurrentFounded.SetIsCurrentFalse();
            }
        }


        var etebarDoreh = EtebarDoreh.Create
               (command.OrgTypeGUID, command.Title, _dateTimeProvider.Now, Guid.Parse(_userContext.UserId),
                command.StartDate, command.EndDate, command.IsCurrent, command.SortOrder);

        _etebarDorehRepository.Add(etebarDoreh);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return etebarDoreh.GUID;
    }
}