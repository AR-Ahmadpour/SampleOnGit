using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.accInstanceMehvarEnvironmentStandardsResults;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards;
using Accreditation.Domain.AccInstanceMehvarEnvironmentStandardsResults.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accreditation.Application.AccInstanceMehvars.AddEvent;
public class AccInstanceMehvarEnvironmentStandardsResultEventHandler : INotificationHandler<AccInstanceMehvarEnvironmentStandardsResultEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccInstanceMehvarRepository _accInstanceMehvarRepository;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IAccInstanceMehvarEnvironmentStandardsResultRepository _accInstanceMehvarEnvironmentStandardsResultRepository;
    private readonly ILogger<AccInstanceMehvarEnvironmentStandardsResultEventHandler> _logger;


    public AccInstanceMehvarEnvironmentStandardsResultEventHandler(IUnitOfWork unitOfWork,
        ILogger<AccInstanceMehvarEnvironmentStandardsResultEventHandler> logger,
                                                                   IAccInstanceMehvarRepository accInstanceMehvarRepository,
                                                                   IAccreditationInstanceRepository accreditationInstanceRepository,
                                                                   IAccInstanceMehvarEnvironmentStandardsResultRepository accInstanceMehvarEnvironmentStandardsResultRepository)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _accInstanceMehvarRepository = accInstanceMehvarRepository;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _accInstanceMehvarEnvironmentStandardsResultRepository = accInstanceMehvarEnvironmentStandardsResultRepository;
    }

    public async Task Handle(AccInstanceMehvarEnvironmentStandardsResultEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            //var environmentStandards = await _environmentStandardRepository.GetGuidListAsync(notification.EtebarDoreGuid);
            
            foreach (var environmentStandardsritem in notification.environmentStandards)
            {
                var accInstanceMehvarResult = AccInstanceMehvarEnvironmentStandardsResult.Create(notification.AccreditationInstanceMehvarGUID,                                    
                                                                                                                environmentStandardsritem);
                _accInstanceMehvarEnvironmentStandardsResultRepository.Add(accInstanceMehvarResult);
            }
            Thread.Sleep(50);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            var accInstanceMehvarz = await _accInstanceMehvarRepository.GetListAccInstanceMehvarAsync(notification.AccreditationInstanceGUID);
            if (accInstanceMehvarz != null)
            {
                foreach (var accInstanceMehvar in accInstanceMehvarz)
                {
                    _accInstanceMehvarRepository.Delete(accInstanceMehvar);
                }
            }
            var evaluation = await _accreditationInstanceRepository.FindAsync(notification.AccreditationInstanceGUID);
            if (evaluation != null)
            {
                _accreditationInstanceRepository.Delete(evaluation);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            throw new Exception(ex.Message);
        }
    }
}