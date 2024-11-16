using Accreditation.Application.Sanjehs.GetList;
using Accreditation.Application.SavabeghShoghlis.GetList;
using Accreditation.Domain.SavabeghShoglis.Entites;

namespace Accreditation.Application.Common.Interfaces.Persistence.SavabeghShoghlis
{
    public interface ISavabeghShoghliRepository
    {
        void Add(SavabeghShoghli savabegh);
        Task<SavabeghShoghli?> FindEditAsync(Guid Guid, CancellationToken cancellationToken);
        Task<SavabeghShoghli?> FindAsync(Guid Guid, CancellationToken cancellationToken);
        Task<List<GetListSavabeghShoghliByUserGuidDto>> GetListByUserIdAsync(Guid userGuid, CancellationToken cancellationToken = default);
    }
}
