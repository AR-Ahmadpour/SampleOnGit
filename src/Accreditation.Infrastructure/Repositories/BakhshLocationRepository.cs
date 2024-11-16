using Accreditation.Domain.CountryDivisions.BakhshLocation.Abstractions;
using Accreditation.Domain.CountryDivisions.BakhshLocation.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class BakhshLocationRepository(AccreditationDbContext context) : IBakhshLocationRepository
{
    public async Task<Bakhsh?> FindAsync(int BakhshId, CancellationToken cancellationToken = default)
    {
        return await context.Bakhsh.FindAsync(BakhshId,cancellationToken);
    }

    public async Task<List<SelectListCountryDevisionResponse>> GetSelectListBakhshLocationAsync(int shahrestanId, CancellationToken cancellationToken = default)
    {
        return await context.Bakhsh
                 .Where(x => x.ShahrestanId == shahrestanId && !x.IsDeleted)
                 .Select(x => new SelectListCountryDevisionResponse(x.Id, x.Title))
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
    }
}