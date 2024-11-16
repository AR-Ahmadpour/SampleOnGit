using Accreditation.Domain.OrgGerayesh.Abstractions;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class OrgGerayeshRepository(AccreditationDbContext context) : IOrgGerayeshRepository
{
    public async Task<bool> FindAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var exists = await context.OrgGerayeshes
     .AnyAsync(x => x.GUID == guid, cancellationToken);

        return exists;
    }

    public async Task<List<SelectListResponse>> GetSelectListOrgGerayeshAsync(Guid orgTypeGuid, CancellationToken cancellationToken = default)
    {
        return await context.OrgGerayeshes
          .Where(x => x.OrgTypeGUID == orgTypeGuid && !x.IsDeleted)
          .Select(x => new SelectListResponse(x.GUID, x.Title))
          .AsNoTracking()
          .ToListAsync(cancellationToken);
    }
}
