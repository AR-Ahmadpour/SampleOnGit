using Accreditation.Application.Common.Interfaces.Persistence.Tahsilats;
using Accreditation.Application.Tahsilats.GetById;
using Accreditation.Application.UserInfos.GetById;
using Accreditation.Domain.Tahsilats.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class TahsilatRepository : ITahsilatRepository
    {
        private readonly AccreditationDbContext _context;

        public TahsilatRepository(AccreditationDbContext context)
        {
            _context = context;
        }

        public void Add(Tahsilat tahsilat)
        {
            _context.Tahsilats.Add(tahsilat);
        }

        public async Task<Tahsilat?> FindAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            return await _context.Tahsilats
                         .FirstOrDefaultAsync(x => x.UserGUID == guid, cancellationToken);
        }

        public async Task<Tahsilat?> FindAsyncEdit(Guid guid, CancellationToken cancellationToken)
        {
            return await _context.Tahsilats
                .FindAsync(guid, cancellationToken);
        }

        public async Task<GetTahsilatByUserGuidDto?> GetTahsilatInfoWithUserDetailsAsync(Guid userGuid, CancellationToken cancellationToken = default)
        {
            var query = from tahsilat in _context.Tahsilats
                        join user in _context.Users on tahsilat.UserGUID equals user.GUID
                        where tahsilat.UserGUID == userGuid
                        select new GetTahsilatByUserGuidDto
                        {
                            Guid = tahsilat.GUID,
                            MaghtaTahsili = tahsilat.MaghtaTahsiliGUID,
                            ReshtehTahsili = tahsilat.ReshtehTahsiliGUID,
                            UniversityName = tahsilat.UniversityName,
                            GraduationDate = tahsilat.GraduationDate,
                            MadrakGUID = tahsilat.MadrakGUID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            NationalCOde = user.NationalCode
                        };

            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
