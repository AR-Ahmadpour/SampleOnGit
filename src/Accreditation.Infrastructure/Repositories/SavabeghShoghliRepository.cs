using Accreditation.Application.Common.Interfaces.Persistence.SavabeghShoghlis;
using Accreditation.Application.Sanjehs.GetList;
using Accreditation.Application.SavabeghShoghlis.GetList;
using Accreditation.Domain.SavabeghShoglis.Entites;
using Accreditation.Domain.Standards.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories
{
    internal class SavabeghShoghliRepository : ISavabeghShoghliRepository
    {
        private readonly AccreditationDbContext _context;

        public SavabeghShoghliRepository(AccreditationDbContext context)
        {
            _context = context;
        }

        public void Add(SavabeghShoghli savabegh)
        {
            _context.SavabeghShoghlis.Add(savabegh);
        }

        public async Task<SavabeghShoghli?> FindAsync(Guid Guid, CancellationToken cancellationToken)
        {
            return await _context.SavabeghShoghlis
                .FirstOrDefaultAsync(x => x.UserGuid == Guid, cancellationToken);
        }

        public async Task<SavabeghShoghli?> FindEditAsync(Guid Guid, CancellationToken cancellationToken)
        {
            return await _context.SavabeghShoghlis.FindAsync(Guid, cancellationToken);
        }

        public async Task<List<GetListSavabeghShoghliByUserGuidDto>> GetListByUserIdAsync(Guid userGuid, CancellationToken cancellationToken = default)
        {
            var query =await _context.SavabeghShoghlis
            .Where(x => x.UserGuid == userGuid)
            .Select(x => new GetListSavabeghShoghliByUserGuidDto
            {
                UserGuid = x.UserGuid,
                Title = x.Title,
                SematId = x.SematId,
                SematTitle = x.Semat.Title,
                SazmanId = x.SazmanId,
                SazmanTitle = x.Sazman.Title,
                NoeKhedmatId =x.NoeKhedmatId,
                NoeKhedmatTitle = x.NoeKhedmat.Title,
                HamkariModelId = x.HamkariModelId,
                HamkariModelTitle = x.HamkariModel.Title,
                OstanId = x.OstanId,
                OstanTitle = x.Ostan.Title,
                ShahrId = x.ShahrId,
                ShahrTitle = x.Shahr.Title,
                StartDate = x.StartDate,
                EndDate = x.EndDate ?? null

            })
            .AsNoTracking()
            .AsQueryable()
            .ToListAsync(cancellationToken);

            return query;
        }
    }
}
