using Accreditation.Application.Tahsilats.GetById;
using Accreditation.Application.UserDorehs.GetById;
using Accreditation.Domain.Tahsilats.Entities;
using Accreditation.Domain.UserDorehs.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.UserDorehs
{
    public interface IUserDorehRepository
    {
        void Add(UserDoreh userDoreh);
        Task<UserDoreh?> FindAsync(Guid guid, CancellationToken cancellationToken = default);
        Task<UserDoreh?> FindEditAsync(Guid guid, CancellationToken cancellationToken);
        Task<List<GetUserDorehByUserGuidDto?>> GetUserDorehInInfoWithUserDetailsAsync(Guid userGuid, CancellationToken cancellationToken = default);
    }
}
