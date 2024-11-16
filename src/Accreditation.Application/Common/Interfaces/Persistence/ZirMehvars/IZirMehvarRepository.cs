using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Models;
using Accreditation.Application.ZirMehvars.Dtos;
using Accreditation.Application.ZirMehvars.GetListZirMehvarsInArzyabiDakheli;
using Accreditation.Domain.ZirMehvars.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;

public interface IZirMehvarRepository
{
    void Add(ZirMehvar zirMehvar);

    void Delete(ZirMehvar zirMehvar);

    Task<List<ZirMehvar>> GetSelectListAsync(Guid etebarDorehGuid);
    Task<ZirMehvar?> FindAsync(Guid guid, CancellationToken cancellationToken = default);

    Task<bool> IsTitleUniqueAsync(Guid? guid, Guid mehvarGuid, string title, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Guid? guid, CancellationToken cancellationToken = default);

    Task<ZirMehvar?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default);

    Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken);
    Task<PagedList<GetListByMehvarIdAsyncDto>> GetListByMehvarIdAsync(Guid mehvarid, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<List<GetListZirMehvarsInArzyabiDakheliDto>> GetListZirMehvarsInArzyabiDakheliAsync(GetListZirMehvarsInArzyabiDakheliQuery query, CancellationToken cancellationToken);
}
