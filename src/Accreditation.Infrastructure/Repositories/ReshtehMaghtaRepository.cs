using Accreditation.Application.Common.Interfaces.Persistence.ReshtehMaghta;
using Accreditation.Application.ReshtehMaghtas.GetList;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class ReshtehMaghtaRepository : IReshtehMaghtaRepository
    {
        private readonly AccreditationDbContext context;

        public ReshtehMaghtaRepository(AccreditationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<GetListReshtehMaghtaQueryDto>> GetReshtehsByMaghtaIdAsync(Guid maghtaGuid, CancellationToken cancellationToken = default)
        {
            return await context.ReshteMaghtas
            .Where(rm => rm.MaghtaTahsiliGUID == maghtaGuid)
            .Select(rm => new GetListReshtehMaghtaQueryDto
            {
                Guid = rm.ReshtehTahsiliGUID,
                Title = rm.ReshteTahsili.Title
            })
            .ToListAsync(cancellationToken);
        }
    }
}
