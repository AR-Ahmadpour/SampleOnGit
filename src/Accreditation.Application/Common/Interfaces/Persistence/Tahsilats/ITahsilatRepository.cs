using Accreditation.Application.Tahsilats.GetById;
using Accreditation.Domain.Tahsilats.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.Tahsilats
{
    public interface ITahsilatRepository
    {
        void Add(Tahsilat tahsilat);
        Task<Tahsilat?> FindAsyncEdit(Guid guid, CancellationToken cancellationToken);
        Task<Tahsilat?> FindAsync(Guid guid, CancellationToken cancellationToken = default);
        Task<GetTahsilatByUserGuidDto?> GetTahsilatInfoWithUserDetailsAsync(Guid userGuid, CancellationToken cancellationToken = default);
    }
}
