using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.AccreditationInstanceStatuses.AddEvent;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationalInstanceAnswers;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstancesEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstanceStatuses;
using Accreditation.Application.Common.Interfaces.Persistence.Sanjehs;
using Accreditation.Domain.AccreditationInstanceAnswers.Entities;
using MediatR;

namespace Accreditation.Application.AccreditationalInstanceAnswers.AddEvent;
public class AccreditationalInstanceAnswerEventHandler : INotificationHandler<AccreditationalInstanceAnswerEvent>
{
    private readonly IAccreditationInstanceStatusRepository _accreditationInstanceStatusRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly ISanjehRepository _sanjehRepository;
    private readonly IAccreditationInstanceAnswerRepository _accreditationInstanceAnswerRepository;
    private readonly IAccreditationInstancesEnvironmentStandardsResultRepository _accrInstancesEnvironmentStandardsResultRepository;

    public AccreditationalInstanceAnswerEventHandler(IAccreditationInstanceStatusRepository accreditationInstanceStatusRepository,
                                                     IMediator mediator,
                                                     IUnitOfWork unitOfWork,
                                                     IAccreditationInstanceRepository accreditationInstanceRepository,
                                                     ISanjehRepository sanjehRepository,
                                                     IAccreditationInstanceAnswerRepository accreditationInstanceAnswerRepository,
                                                     IAccreditationInstancesEnvironmentStandardsResultRepository accrInstancesEnvironmentStandardsResultRepository)
    {
        _accreditationInstanceStatusRepository = accreditationInstanceStatusRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _sanjehRepository = sanjehRepository;
        _accreditationInstanceAnswerRepository = accreditationInstanceAnswerRepository;
        _accrInstancesEnvironmentStandardsResultRepository = accrInstancesEnvironmentStandardsResultRepository;
    }

    public async Task Handle(AccreditationalInstanceAnswerEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var Sanjehs = await _sanjehRepository.GetByEtebarDorehGuidAsync(notification.EtebarDoreGuid);

            Sanjehs.ForEach(sanjeh =>
            {
                var accInstanceAnswer = AccreditationalInstanceAnswer.Create(notification.AccreditationInstanceGUID,
                                                                             sanjeh.GUID);
                _accreditationInstanceAnswerRepository.Add(accInstanceAnswer);
            });

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Publish the event
            await _mediator.Publish(new AccreditationInstanceStatusesEvent(notification.AccreditationInstanceGUID));
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();

            var accStatus = await _accreditationInstanceStatusRepository.FindBasedAccInstanceAsyc(notification.AccreditationInstanceGUID, cancellationToken);
            if (accStatus != null)
            {
                _accreditationInstanceStatusRepository.Delete(accStatus);
            }

            var accAnswer = await _accreditationInstanceAnswerRepository.FindAsyc(notification.AccreditationInstanceGUID, cancellationToken);
            if (accAnswer != null)
            {
                _accreditationInstanceAnswerRepository.Delete(accAnswer);
            }


            var evaluation = await _accreditationInstanceRepository.FindAsync(notification.AccreditationInstanceGUID);
            if (evaluation != null)
            {
                _accreditationInstanceRepository.Delete(evaluation);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            throw new Exception(ex.Message) ;
        }
    }
}