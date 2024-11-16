using Accreditation.Application.Common.Interfaces.Persistence.Semats;
using Accreditation.Application.Semats.GetList;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class SematRepository : ISematRepository
    {
        private readonly AccreditationDbContext _context;

        public SematRepository(AccreditationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetListSematDto>> GetListAsync(CancellationToken cancellationToken = default)
        {
            var query = await _context.Semats.Select(x => new GetListSematDto
            {
                Id = x.Id,
                Title = x.Title,
            })
            .AsNoTracking()
            .AsQueryable()
            .ToListAsync();

            return query;
        }
    }
}
