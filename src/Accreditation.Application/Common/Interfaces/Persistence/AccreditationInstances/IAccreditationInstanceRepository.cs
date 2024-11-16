using Accreditation.Application.AccreditationInstances.GetByEtebarDorehGUID;
using Accreditation.Application.AccreditationInstances.GetList;
using Accreditation.Application.AccreditationInstances.GetListBasedMasters;
using Accreditation.Application.AccreditationInstances.GetSelectLists;
using Accreditation.Application.Body.GetByAccreditationInstanceGuid;
using Accreditation.Domain.AccreditationInstances.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;

public interface IAccreditationInstanceRepository
{
    Task<AccreditationInstance?> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task<GetBodyByAccreditationInstanceGuidDto> GetBody(Guid accreditationInstanceGuid,Guid FieldGuid, CancellationToken cancellationToken);
    Task<GetBodyByAccreditationInstanceGuidMommayeziDto> GetBodyMommayezi(Guid accreditationInstanceGuid,Guid FieldGuid, CancellationToken cancellationToken);
    Task<bool> Any(Guid id, CancellationToken cancellationToken = default);
    Task<bool> FindIsPayehAsync(Guid masterGuid, CancellationToken cancellationToken = default);
    Task<List<GetListBasedMasterDto>> FindAllBasedPayehAsync(Guid masterGuid, CancellationToken cancellationToken = default);

    Task<AccreditationInstance?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Delete(AccreditationInstance accreditationInstance);
    void Add(AccreditationInstance accreditationInstance);
    Task<List<GetListAccreditationalInstanceDto>> GetAllAsync(int instanceTypeId,
                                                       Guid etebarDorehGUID,
                                                       Guid organizationGuid,
                                                       CancellationToken cancellationToken = default);
    Task<List<GetListPayehAccreditationalInstanceDto>> GetAllPayehAsync(int instanceTypeId,
                                                      Guid etebarDorehGUID,
                                                      Guid organizationGuid,
                                                      CancellationToken cancellationToken = default);
    Task<List<GetAccreditationInstanceByEtebarDorehIdQueryDto>> GetAccreditationInstanceByEtebarDorehId(GetAccreditationInstanceByEtebarDorehIdQuery query, CancellationToken cancellationToken = default);

}