using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstancesEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Domain.Common.Enums;
using Accreditation.Domain.EvaluationArzyabs.Entities;
using MediatR;
using SharedKernel;

namespace Accreditation.Application.EvaluationArzyabs.AddEvent;
public class EvaluationArzyabEventHandler : INotificationHandler<EvaluationArzyabEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IEnvironmentStandardRepository _environmentStandardRepository;
    private readonly IEvaluationArzyabRepository _evaluationArzyabRepository;
    private readonly IDateTimeProvider _dateTime;
    private readonly IAccreditationInstancesEnvironmentStandardsResultRepository _accrInstancesEnvironmentStandardsResultRepository;

    public EvaluationArzyabEventHandler(IUnitOfWork unitOfWork,
                                        ICurrentUser currentUser,
                                        IAccreditationInstanceRepository accreditationInstanceRepository,
                                        IEnvironmentStandardRepository environmentStandardRepository,
                                        IEvaluationArzyabRepository evaluationArzyabRepository,
                                        IDateTimeProvider dateTime,
                                        IAccreditationInstancesEnvironmentStandardsResultRepository accrInstancesEnvironmentStandardsResultRepository)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _environmentStandardRepository = environmentStandardRepository;
        _evaluationArzyabRepository = evaluationArzyabRepository;
        _dateTime = dateTime;
        _accrInstancesEnvironmentStandardsResultRepository = accrInstancesEnvironmentStandardsResultRepository;
    }

    public async Task Handle(EvaluationArzyabEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var EvaluatArzyab = EvaluationArzyab.Create(Guid.Parse(_currentUser.UserId),
                                                        notification.AccreditationInstanceGUID,
                                                        notification.ArzyabUserGUID ?? Guid.Empty,
                                                        (int)ArzyabRole.Sarparast,
                                                        _dateTime.Now);
            _evaluationArzyabRepository.Add(EvaluatArzyab);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();

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