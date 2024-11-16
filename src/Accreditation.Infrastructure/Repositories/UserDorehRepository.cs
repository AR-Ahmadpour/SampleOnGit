using Accreditation.Application.Common.Interfaces.Persistence.UserDorehs;
using Accreditation.Application.Tahsilats.GetById;
using Accreditation.Application.UserDorehs.GetById;
using Accreditation.Domain.UserDorehs.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories
{
    internal class UserDorehRepository : IUserDorehRepository
    {
        private readonly AccreditationDbContext _context;

        public UserDorehRepository(AccreditationDbContext context)
        {
            _context = context;
        }

        public void Add(UserDoreh userDoreh)
        {
            _context.UserDorehs.Add(userDoreh);
        }

        public async Task<UserDoreh?> FindAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            return await _context.UserDorehs.FirstOrDefaultAsync(x => x.UserGuid == guid, cancellationToken);
        }

        public async Task<UserDoreh?> FindEditAsync(Guid guid, CancellationToken cancellationToken)
        {
            return await _context.UserDorehs.FindAsync(guid, cancellationToken);
        }

        public async Task<List<GetUserDorehByUserGuidDto?>> GetUserDorehInInfoWithUserDetailsAsync(Guid userGuid, CancellationToken cancellationToken = default)
        {
            var query = from userdoreh in _context.UserDorehs
                        join user in _context.Users on userdoreh.UserGuid equals user.GUID
                        where userdoreh.UserGuid == userGuid
                        select new GetUserDorehByUserGuidDto
                        {
                            Guid = userdoreh.GUID,
                            DorehAmoozeshiGUID = userdoreh.DorehAmoozeshiGuid,
                            DorehAmoozeshiTitle = userdoreh.DorehAmoozeshi.Title,
                            BargozarKonandeh = userdoreh.BargozarKonandeh,
                            DorehTitle = userdoreh.DorehTitle,
                            DorehHours = userdoreh.DorehHours,
                            DorehRole = userdoreh.DorehRole,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            NationalCode = user.NationalCode
                        };

            return await query.ToListAsync(cancellationToken);
        }
    }
}
