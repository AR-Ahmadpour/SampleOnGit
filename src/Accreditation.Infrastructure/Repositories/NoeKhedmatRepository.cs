using Accreditation.Application.Common.Interfaces.Persistence.NoeKhedmats;
using Accreditation.Application.NoeKhedmats.GetList;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;


namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class NoeKhedmatRepository : INoeKhedmatRepository
    {
        private readonly AccreditationDbContext _context;

        public NoeKhedmatRepository(AccreditationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetListNoeKhedmatDto>> GetListAsync(CancellationToken cancellationToken = default)
        {
            var result = await _context.
                NoeKhedmats.Select(x => new GetListNoeKhedmatDto
                {
                    Id = x.Id,
                    Title = x.Title

                })
                .AsNoTracking()
                .AsQueryable()
                .ToListAsync();


            return result;
        }
    }
}
