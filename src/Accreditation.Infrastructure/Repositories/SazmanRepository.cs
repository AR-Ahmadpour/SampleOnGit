using Accreditation.Application.Common.Interfaces.Persistence.Sazmans;
using Accreditation.Application.Sazmans.GetList;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class SazmanRepository : ISazmanRepository
    {
        private readonly AccreditationDbContext _context;

        public SazmanRepository(AccreditationDbContext context)
        {
            _context = context;
        }

        public Task<List<GetListSazmanDto>> GetListAsync(CancellationToken cancellationToken = default)
        {
            var query = _context.Sazmans
            .Select(x => new GetListSazmanDto
            {
                Title = x.Title,
                Id = x.Id,
            })
            .AsNoTracking()
            .AsQueryable()
            .ToListAsync(cancellationToken);

            return query;
        }
    }
}
