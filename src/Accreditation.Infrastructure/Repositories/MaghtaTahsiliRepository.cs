using Accreditation.Application.Common.Interfaces.Persistence.MaghtaTahsilis;
using Accreditation.Application.MaghtaTahsilis.GetList;
using Accreditation.Domain.MaghtaTahsilis.Entities;
using Accreditation.Domain.ReshteMaghtas.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class MaghtaTahsiliRepository : IMaghtaTahsiliRepository
    {
        private readonly AccreditationDbContext context;

        public MaghtaTahsiliRepository(AccreditationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> FindAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            return await context.ReshteMaghtas.AnyAsync(x => x.MaghtaTahsiliGUID == guid);
        }

        public async Task<List<GetAllMaghtaTahsiliQueryDto>> GetListMaghtaTahsiliAsync(CancellationToken cancellationToken = default)
        {
            var query = await context.MaghtaTahsilis
            .AsNoTracking()
            .Select(x => new GetAllMaghtaTahsiliQueryDto
            {
                Guid = x.GUID,
                Title = x.Title
            })
            .ToListAsync(cancellationToken);

            return query;
        }


    }
}
