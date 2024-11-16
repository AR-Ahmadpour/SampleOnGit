using Accreditation.Application.UserInfos.GetById;
using Accreditation.Domain.Sanjehs.Entities;
using Accreditation.Domain.UserInfos.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.UserInfos
{
    public interface IUserInfoRepository
    {
        void Add(UserInfo userInfo);
        Task<UserInfo?> FindAsync(Guid guid, CancellationToken cancellationToken = default);
        Task<UserInfo?> FindEditAsync(Guid guid, CancellationToken cancellationToken = default);
        Task<GetUserInfoByUserGuidDto?> GetUserInfoWithUserDetailsAsync(Guid userGuid, CancellationToken cancellationToken = default);
    }
}
