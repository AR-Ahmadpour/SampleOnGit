
using Accreditation.Domain.CountryDivisions.Ostans.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class OstanRepository(AccreditationDbContext context) : IOstanRepository
{
    public async Task<Ostan?> FindAsync(int OstanId, CancellationToken cancellationToken = default)
    {
        return await context.Ostans.FindAsync(OstanId, cancellationToken);
    }

    public async Task<List<SelectListCountryDevisionResponse>> GetSelectListByOstanIdAsync( CancellationToken cancellationToken = default)
    {
        return await context.Ostans
                 .Where(x => !x.IsDeleted)
                 .Select(x => new SelectListCountryDevisionResponse(x.Id, x.Title))
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
    }
}