using Accreditation.Application.Common.Models;
using Accreditation.Application.Fields.GetById;
using Accreditation.Application.Fields.GetFilterdList;
using Accreditation.Application.Fields.GetList;
using Accreditation.Domain.Fields.Entities;
using Accreditation.Domain.Mehvars.Entities;
using Accreditation.Domain.SanjehFields.Entities;
using Accreditation.Domain.Sanjehs.Entities;
using Accreditation.Domain.Standards.Entities;
using Accreditation.Domain.ZirMehvars.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
public interface IFieldRepository
{
    void Add(Field field);
    Task<PagedList<GetListByEtebarDorehIdDto>> GetListByEtebarDorehIdAsync(Guid etebarDorehId, int PageNumber,int Pagesize,CancellationToken cancellationToken = default);
    Task<List<GetAllFilteredFieldQueryDto>> GetAllFilteredByEtebarDorehGuidAsync(Guid etebarDorehGuid, List<Guid> EvaluationArzyabsFields, int instanceTypeId);
    Task<List<GetAllFieldQueryDto>> GetAllByEtebarDorehGuidAsync(Guid etebarDorehGuid, int instanceTypeId);
    Task<Field?> FindAsync(Guid guid, CancellationToken cancellationToken = default);
    Task<Mehvar> GetMehvarByIdAsync(Guid mehvarId);
    Task<List<ZirMehvar>> GetZirmehvarsByMehvarGuidAsync(Guid mehvarGuid);
    Task<List<Standard>> GetStandardsByZirmehvarIdAsync(Guid zirmehvarId);
    Task<List<Sanjeh>> GetSanjehsByStandardIdAsync(Guid standardId);

}
