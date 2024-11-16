using Accreditation.Application.Common.Interfaces.Persistence.UserInfos;
using Accreditation.Application.UserInfos.GetById;
using Accreditation.Domain.UserInfos.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories
{
    internal class UserInfoRepository : IUserInfoRepository
    {
        private readonly AccreditationDbContext _context;

        public UserInfoRepository(AccreditationDbContext context)
        {
            _context = context;
        }

        public void Add(UserInfo userInfo)
        {
            _context.UserInfos.Add(userInfo);
        }

        public async Task<UserInfo?> FindAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            return await _context.UserInfos
                         .FirstOrDefaultAsync(x => x.UserGuid == guid, cancellationToken);
        }

        public async Task<UserInfo?> FindEditAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            return await _context.UserInfos
                         .FindAsync(guid, cancellationToken);
        }

        public async Task<GetUserInfoByUserGuidDto?> GetUserInfoWithUserDetailsAsync(Guid userGuid, CancellationToken cancellationToken = default)
        {
            var query = from userInfo in _context.UserInfos
                        join user in _context.Users on userInfo.UserGuid equals user.GUID
                        where userInfo.UserGuid == userGuid
                        select new GetUserInfoByUserGuidDto
                        {
                            Guid = userInfo.GUID,
                            MaritalStatus = userInfo.MaritalStatus,
                            ChildCount = userInfo.ChildCount,
                            BirthOstandId = userInfo.BirthOstanId,
                            BirthShahrId = userInfo.BirthShahrId,
                            AddressOstanId = userInfo.AddressOstanId,
                            AddressShahrId = userInfo.AddressShahrId,
                            Address = userInfo.Address,
                            PersonalPicGUID = userInfo.PersonalPicGUID,
                            KartMeliGUID = userInfo.KartMeliGUID,
                            ShenasnamehGUID = userInfo.ShenasnamehGUID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            NationalCOde = user.NationalCode
                        };

            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }

}
