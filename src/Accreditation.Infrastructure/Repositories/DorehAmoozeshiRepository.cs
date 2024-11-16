using Accreditation.Application.Common.Interfaces.Persistence.DorehAmoozeshis;
using Accreditation.Application.DorehAmoozeshis.GetList;
using Accreditation.Application.Sanjehs.GetList;
using Accreditation.Domain.DorehAmoozeshis.Entities;
using Accreditation.Domain.Standards.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories
{
    public sealed class DorehAmoozeshiRepository : IDorehAmoozeshiRepository
    {
        private readonly AccreditationDbContext _context;

        public DorehAmoozeshiRepository(AccreditationDbContext context)
        {
            _context = context;
        }

        public async Task<DorehAmoozeshi?> FindAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            return await _context
                .DorehAmoozeshis.FindAsync(guid, cancellationToken);
        }

        public async Task<List<GetListDorehAmoozeshiDto>> GetListDorehAmoozeshiAsync(CancellationToken cancellationToken = default)
        {
            var query = _context.DorehAmoozeshis
            .Select(x => new GetListDorehAmoozeshiDto
            {
                Title = x.Title,
                Guid = x.GUID,

            })
            .AsNoTracking()
            .AsQueryable()
            .ToListAsync(cancellationToken);

            return await query;
        }
    }
}
