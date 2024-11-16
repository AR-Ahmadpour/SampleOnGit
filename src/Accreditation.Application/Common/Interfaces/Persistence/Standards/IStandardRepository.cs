using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Standards.GetListStandardsInArzyabiDakheli;
using Accreditation.Application.ZirMehvars.GetListZirMehvarsInArzyabiDakheli;
using Accreditation.Domain.Standards.Dtos;
using Accreditation.Domain.Standards.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.Standards;

public interface IStandardRepository
{
    void Add(Standard standard);

    void Delete(Standard standard);
    Task<List<Standard>> GetSelectListAsync(Guid etebarDorehGuid);
    Task<Standard?> FindAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Guid? id, CancellationToken cancellationToken = default);

    Task<bool> IsTitleUniqueAsync(Guid? guid, Guid zirMehvarGuid, string title, CancellationToken cancellationToken = default);

    Task<bool> IsCodeUnique(Guid? guid, Guid zirMehvarGuid, string code, CancellationToken cancellationToken = default);

    Task<Standard?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken);
    Task<PagedList<GetAllByZirMehvarIdAsyncDto>> GetListByZirMehvarIdAsync(Guid zirMehvarGUID, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<List<GetListStandardsInArzyabiDakheliDto>> GetListZirMehvarsInArzyabiDakheliAsync(GetListStandardsInArzyabiDakheliQuery query, CancellationToken cancellationToken);

}
