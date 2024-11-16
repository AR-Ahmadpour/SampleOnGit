using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceStandards;
using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceZirMehvars;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
namespace Accreditation.Application.Common.Events;
public class EventRollBack
{
    private readonly IAccInstanceZirMehvarRepository _accInstanceZirMehvarRepository;
    private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
    private readonly IAccInstanceStandardRepository _accInstanceStandardRepository;
    private readonly IAccInstanceMehvarRepository _accInstanceMehvarRepository;

    public EventRollBack(IAccInstanceZirMehvarRepository accInstanceZirMehvarRepository,
                         IAccreditationInstanceRepository accreditationInstanceRepository,
                         IAccInstanceStandardRepository accInstanceStandardRepository,
                         IAccInstanceMehvarRepository accInstanceMehvarRepository)
    {
        _accInstanceZirMehvarRepository = accInstanceZirMehvarRepository;
        _accreditationInstanceRepository = accreditationInstanceRepository;
        _accInstanceStandardRepository = accInstanceStandardRepository;
        _accInstanceMehvarRepository = accInstanceMehvarRepository;
    }
    public async Task DeleteItemsBasedOnCreatedEvents(Guid accreditationInstanceGUID)
    {
        var accInstanceZirMehvarz = await _accInstanceZirMehvarRepository.GetListAccInstanceZirMehvarAsync(accreditationInstanceGUID);
        if (accInstanceZirMehvarz != null)
        {
            foreach (var accInstanceZirMehvar in accInstanceZirMehvarz)
            {
                _accInstanceZirMehvarRepository.Delete(accInstanceZirMehvar);
            }
        }

        var accInstanceMehvarz = await _accInstanceMehvarRepository.GetListAccInstanceMehvarAsync(accreditationInstanceGUID);
        if (accInstanceMehvarz != null)
        {
            foreach (var accInstanceMehvar in accInstanceMehvarz)
            {
                _accInstanceMehvarRepository.Delete(accInstanceMehvar);
            }
        }
        var accreditationInstance = await _accreditationInstanceRepository.FindAsync(accreditationInstanceGUID);
        if (accreditationInstance != null)
        {
            _accreditationInstanceRepository.Delete(accreditationInstance);
        }

    }
}
