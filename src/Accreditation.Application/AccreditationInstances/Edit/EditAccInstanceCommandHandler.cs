using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Domain.Common.Enums;
using Accreditation.Domain.EvaluationArzyabs.Entities;
using MediatR;
using SharedKernel;

namespace Accreditation.Application.AccreditationInstances.Edit;
internal sealed class EditAccInstanceCommandHandler : ICommandHandler<EditAccInstanceCommand, Guid>
{
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEvaluationArzyabRepository _evaluationArzyabRepository;

    public EditAccInstanceCommandHandler(IAccreditationInstanceRepository accreditationInstanceRepository,
                                        IMediator mediator,
                                        IUnitOfWork unitOfWork,
                                        ICurrentUser currentUser,
                                        IDateTimeProvider dateTimeProvider,
                                        IEvaluationArzyabRepository evaluationArzyabRepository)
    {
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _dateTimeProvider = dateTimeProvider;
        _evaluationArzyabRepository = evaluationArzyabRepository;
    }
    public async Task<Result<Guid>> Handle(EditAccInstanceCommand command, CancellationToken cancellationToken)
    {
        var accreditationInstance = await _accreditationInstanceRepository.FindAsync(command.GUID, cancellationToken);
        var evaluationArzyabs = await _evaluationArzyabRepository.GetAllBasedAccins(accreditationInstance.GUID, cancellationToken);
        if (accreditationInstance == null)
        {
            return Result.Failure<Guid>(AccreditationInstanceErrors.NotFound);
        }
        if (command.ArzyabSarparastGuid is not null && accreditationInstance.InstanceTypeId == (int)InstanceTypes.ArzyabiDakheli)
        {
            return Result.Failure<Guid>(AccreditationInstanceErrors.ConflictTypeAccreditation);
        }

        accreditationInstance.Edit(command.FromDate, command.ToDate,Guid.Parse(_currentUser.UserId), _dateTimeProvider.Now);

        if (command.ArzyabSarparastGuid is not null && evaluationArzyabs.Count() == 0)
        {
            var EvaluatArzyab = EvaluationArzyab.Create(Guid.Parse(_currentUser.UserId),
                                                     accreditationInstance.GUID,
                                                     command.ArzyabSarparastGuid.Value,
                                                     (int)ArzyabRole.Sarparast,
                                                     _dateTimeProvider.Now);
            _evaluationArzyabRepository.Add(EvaluatArzyab);

        }


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return accreditationInstance.GUID;
    }
}
