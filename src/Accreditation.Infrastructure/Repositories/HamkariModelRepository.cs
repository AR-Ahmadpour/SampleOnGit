using Accreditation.Application.Common.Interfaces.Persistence.HamkariModels;
using Accreditation.Application.HamkariModels.GetList;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class HamkariModelRepository : IHamkariModelRepository
    {
        private readonly AccreditationDbContext _context;

        public HamkariModelRepository(AccreditationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetListHamkariModelDto>> GetListAsync(CancellationToken cancellationToken = default)
        {
            var query = await _context.HamkariModels
                .Select(x => new GetListHamkariModelDto
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .AsNoTracking()
                .AsQueryable()
                .ToListAsync();

            return query;
        }
    }
}
