using Accreditation.Application.NotNaSanjehs.GetList;
using Accreditation.Domain.NotNaSanjehs.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.NotNaSanjehs
{
    public interface INotNaSanjehRepository
    {
        void Add(NotNaSanjeh notNaSanjeh);
        Task<List<GetListOrgGerayeshBySanjehIdDto>> GetAllOrgGerayeshBySanjehId(Guid SanjehGuid, CancellationToken cancellationToken = default);
        Task<bool> IsOrgGerayeshAssignedToSanjeh(Guid sanjehGuid, Guid orgGerayeshGuid, CancellationToken cancellationToken);
        Task<List<Guid>> GetOrgGerayeshGuidsBySanjehId(Guid sanjehGuid, CancellationToken cancellationToken);
        void RemoveRange(IEnumerable<NotNaSanjeh> notNaSanjehs);
        Task<List<NotNaSanjeh>> GetBySanjehGuidAsync(Guid sanjehGuid, CancellationToken cancellationToken);
    }
}
