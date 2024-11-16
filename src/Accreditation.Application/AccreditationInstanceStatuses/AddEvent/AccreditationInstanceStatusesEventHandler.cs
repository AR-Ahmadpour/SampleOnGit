using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstanceStatuses;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Domain.AccreditationInstanceStatuses;
using MediatR;
using SharedKernel;

namespace Accreditation.Application.AccreditationInstanceStatuses.AddEvent;
public class AccreditationInstanceStatusesEventHandler : INotificationHandler<AccreditationInstanceStatusesEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccreditationInstanceStatusRepository _accreditationInstanceStatusRepository;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AccreditationInstanceStatusesEventHandler(IUnitOfWork unitOfWork,
                                                     IAccreditationInstanceStatusRepository accreditationInstanceStatusRepository,
                                                     IAccreditationInstanceRepository accreditationInstanceRepository,
                                                     ICurrentUser currentUser,
                                                     IDateTimeProvider dateTimeProvider)
    {
        _unitOfWork = unitOfWork;
        _accreditationInstanceStatusRepository = accreditationInstanceStatusRepository;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _currentUser = currentUser;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(AccreditationInstanceStatusesEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var accreditationInstance = await _accreditationInstanceRepository.FindAsync(notification.AccreditationInstanceGUID);
            var accInstanceEnvironmentStandardResult = AccreditationInstanceStatus.Create(notification.AccreditationInstanceGUID,
                                                                                          Guid.Parse(_currentUser.UserId),
                                                                                          _dateTimeProvider.Now,
                                                                                          accreditationInstance.AccreditationInstanceStatusTypeId);

            _accreditationInstanceStatusRepository.Add(accInstanceEnvironmentStandardResult);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();

            var accStatus = await _accreditationInstanceStatusRepository.FindBasedAccInstanceAsyc(notification.AccreditationInstanceGUID, cancellationToken);
            if (accStatus != null)
            {
                _accreditationInstanceStatusRepository.Delete(accStatus);
            }

            var evaluation = await _accreditationInstanceRepository.FindAsync(notification.AccreditationInstanceGUID);
            if (evaluation != null)
            {
                _accreditationInstanceRepository.Delete(evaluation);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            throw;
        }
    }
}