using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.AccInstanceMehvars.AddEvent;
using Accreditation.Application.AccreditationalInstanceAnswers.AddEvent;
using Accreditation.Application.AccreditationInstancesEnvironmentStandardsResultEvents.AddEvent;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstanceStatusTypes;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Persistence.Organization;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.EtebarDorehs;
using Accreditation.Application.EvaluationArzyabs.AddEvent;
using Accreditation.Application.Organizations;
using Accreditation.Domain.AccreditationInstances.Entities;
using Accreditation.Domain.Common.Enums;
using Accreditation.Domain.Users;
using MediatR;
using SharedKernel;

namespace Accreditation.Application.AccreditationInstances.Add;

internal sealed class AddAccreditationalInstanceCommandHandler :
    ICommandHandler<AddAccreditationalInstanceCommand, Guid>
{

    private readonly IAccreditationInstanceRepository _accreditationalInstanceRepository;
    private readonly ICurrentUser _contextUser;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMediator _mediator;
    private readonly IEtebarDorehRepository _etebarDorehRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccreditationInstanceStatusTypeRepository _accreditationInstanceStatusTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddAccreditationalInstanceCommandHandler(IUnitOfWork unitOfWork,
                                                    IAccreditationInstanceRepository accreditationalInstanceRepository,
                                                    ICurrentUser contextUser,
                                                    IDateTimeProvider dateTimeProvider,
                                                    IMediator mediator,
                                                    IEtebarDorehRepository etebarDorehRepository,
                                                    IOrganizationRepository organizationRepository,
                                                    IUserRepository userRepository,
                                                    IAccreditationInstanceStatusTypeRepository accreditationInstanceStatusTypeRepository)
    {
        _accreditationalInstanceRepository = accreditationalInstanceRepository;
        _contextUser = contextUser;
        _dateTimeProvider = dateTimeProvider;
        _mediator = mediator;
        _etebarDorehRepository = etebarDorehRepository;
        _organizationRepository = organizationRepository;
        _userRepository = userRepository;
        _accreditationInstanceStatusTypeRepository = accreditationInstanceStatusTypeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddAccreditationalInstanceCommand command, CancellationToken cancellationToken)
    {
        if ((command.InstanceTypeId == (int)InstanceTypes.ArzyabiDakheli ||
            command.InstanceTypeId == (int)InstanceTypes.ArzyabiJame) &&
            (command.FromDate is null || command.ToDate is null))
        {
            return Result.Failure<Guid>(AccreditationInstanceErrors.FromToDateRequiered);
        }
        else if (command.InstanceTypeId == (int)InstanceTypes.ArzyabiDakheli  && command.ArzyabSarparastGuid != null)
        {
            return Result.Failure<Guid>(AccreditationInstanceErrors.ConflictTypeAccreditation);
        }
        else if (command.InstanceTypeId == (int)InstanceTypes.ArzyabiJame && command.ArzyabSarparastGuid == null)
        {
            return Result.Failure<Guid>(AccreditationInstanceErrors.AccreditationSarparast);
        }
        else if ((command.InstanceTypeId == (int)InstanceTypes.ArzyabiMojadad ||
                  command.InstanceTypeId == (int)InstanceTypes.RastiAzmai)
            && command.AccreditationInstanceGuid == null)
        {
            return Result.Failure<Guid>(AccreditationInstanceErrors.AccreditationPayeh);
        }
        if (await _etebarDorehRepository.FindAsync(command.EtebarDorehGUID, cancellationToken) is null)
        {
            return Result.Failure<Guid>(EtebarDorehErrors.NotFound);
        }
        if (! await _organizationRepository.IsExistAsync(command.OrganizationGuid, cancellationToken))
        {
            return Result.Failure<Guid>(OrganizationErrors.NotFound);
        }

        if (command.ArzyabSarparastGuid != null &&
           await _userRepository.GetByIdAsync(command.ArzyabSarparastGuid.Value, cancellationToken) == null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }
        var accInstanseStatusType = await _accreditationInstanceStatusTypeRepository.FindBasedInstancetypeIdAsyc(command.InstanceTypeId, 0, cancellationToken);
       
        var accreditationalInstance = AccreditationInstance.Create(command.EtebarDorehGUID,
                                                                   command.OrganizationGuid,
                                                                   command.InstanceTypeId,
                                                                   command.FromDate,
                                                                   command.ToDate,
                                                                    _dateTimeProvider.Now,
                                                                   Guid.Parse(_contextUser.UserId),
                                                                   command.AccreditationInstanceGuid,
                                                                   accInstanseStatusType.Id,
                                                                   accInstanseStatusType.IsLocked);

        _accreditationalInstanceRepository.Add(accreditationalInstance);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Publish the event
        await _mediator.Publish(new AccreditationInstancesEnvironmentStandardsResultEvent(command.EtebarDorehGUID,
                                                                                  accreditationalInstance.GUID));

        await _mediator.Publish(new AccInstanceMehvarCreatedEvent(accreditationalInstance.GUID,
                                                                  command.EtebarDorehGUID),
                                                                  cancellationToken);

        if (command.ArzyabSarparastGuid != null)
        {
            await _mediator.Publish(new EvaluationArzyabEvent(command.EtebarDorehGUID,
                                                         accreditationalInstance.GUID,
                                                         command.ArzyabSarparastGuid),
                                                         cancellationToken);
        }

        //publish AccreditationalInstanceAnswerEvent 
        await _mediator.Publish(new AccreditationalInstanceAnswerEvent(command.EtebarDorehGUID,
                                                                       accreditationalInstance.GUID));

        return accreditationalInstance.GUID;
    }
}
