using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Sanjehs.GetList;
using Accreditation.Application.Sanjehs.GetSanjehListInArzyabiDakheli;
using Accreditation.Application.Sanjehs.GetSanjehValidResault;
using Accreditation.Domain.Sanjehs.Entities;


namespace Accreditation.Application.Common.Interfaces.Persistence.Sanjehs
{
    public interface ISanjehRepository
    {
        void Add(Sanjeh sanjeh);

        Task<Sanjeh?> FindAsync(Guid guid, CancellationToken cancellationToken = default);

        Task<List<Sanjeh>> GetByEtebarDorehGuidAsync(Guid etebarDorehGuid, CancellationToken cancellationToken = default);

        Task<PagedList<GetListByStandardIdAsyncDto>> GetListByStandardIdAsync(int PageNumber,int PageSize,Guid standardId, CancellationToken cancellationToken = default);

        Task<List<GetBreadCrumbDto>> FetchBreadCrumbDataAsync(Guid? guid, CancellationToken cancellationToken);
        Task<Sanjeh?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default);
        Task<List<GetSanjehListInArzyabiDakheliDto>> GetSanjehListInArzyabiDakheli(GetSanjehListInArzyabiDakheliQuery query, CancellationToken cancellationToken = default);
        Task<List<GetSanjehValidResaultDto>> GetSanjehValidResault(GetSanjehValidResaultQuery query, CancellationToken cancellationToken = default);
    }
}
