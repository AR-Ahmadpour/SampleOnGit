using Accreditation.Domain.CountryDivisions.Ostans.Entities;
using Accreditation.Domain.CountryDivisions.Shahr.Abstractions;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class ShahrRepository(AccreditationDbContext context) : IShahrRepository
{
    public async Task<List<SelectListCountryDevisionResponse>> GetSelectListSharAsync(int OstanId, CancellationToken cancellationToken = default)
    {
        return await context.Shahrs
                 .Where(x => x.OstanId == OstanId && !x.IsDeleted)
                 .Select(x => new SelectListCountryDevisionResponse(x.Id, x.Title))
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
    }

    public async Task<List<SelectListCountryDevisionResponse>> GetSelectListSharByBakhshAsync(int BakhshId, CancellationToken cancellationToken = default)
    {
        return await context.Shahrs
                 .Where(x => x.BakhshId == BakhshId && !x.IsDeleted)
                 .Select(x => new SelectListCountryDevisionResponse(x.Id, x.Title))
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
    }
}