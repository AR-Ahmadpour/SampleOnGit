using Accreditation.Application.Common.Interfaces.Persistence.NotNaSanjehs;
using Accreditation.Application.NotNaSanjehs.GetList;
using Accreditation.Application.SanjehEnvironmentStandards.GetList;
using Accreditation.Domain.NotNaSanjehs.Entities;
using Accreditation.Domain.Sanjehs.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class NotNaSanjehRepository(AccreditationDbContext context) : INotNaSanjehRepository
    {
        
        public void Add(NotNaSanjeh notNaSanjeh)
        {
            context.NotNaSanjehs.Add(notNaSanjeh);
        }

        public async Task<List<GetListOrgGerayeshBySanjehIdDto>> GetAllOrgGerayeshBySanjehId(Guid SanjehGuid, CancellationToken cancellationToken = default)
        {
            var result = await context.NotNaSanjehs
            .Where(x => x.SanjehGUID == SanjehGuid)
            .Select(c => new GetListOrgGerayeshBySanjehIdDto
            {
                Guid = c.OrgGerayeshGUID,
                Title = c.OrgGerayesh.Title
                
            }).ToListAsync();

            return result;

        }

        public async Task<List<NotNaSanjeh>> GetBySanjehGuidAsync(Guid sanjehGuid, CancellationToken cancellationToken)
        {
            return await context.Set<NotNaSanjeh>()
                     .Where(n => n.SanjehGUID == sanjehGuid)
                     .ToListAsync(cancellationToken);
        }

        public async Task<List<Guid>> GetOrgGerayeshGuidsBySanjehId(Guid sanjehGuid, CancellationToken cancellationToken)
        {
            return await context.NotNaSanjehs
                .Where(nn => nn.SanjehGUID == sanjehGuid)
                .Select(nn => nn.OrgGerayeshGUID)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsOrgGerayeshAssignedToSanjeh(Guid sanjehGuid, Guid orgGerayeshGuid, CancellationToken cancellationToken)
        {
            return await context.NotNaSanjehs
           .AnyAsync(x => x.SanjehGUID == sanjehGuid && x.OrgGerayeshGUID == orgGerayeshGuid, cancellationToken);
        }

        public void RemoveRange(IEnumerable<NotNaSanjeh> notNaSanjehs)
        {
            context.Set<NotNaSanjeh>().RemoveRange(notNaSanjehs);
        }
    }
}
