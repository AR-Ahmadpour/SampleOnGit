using Accreditation.Domain.CountryDivisions.Shahrestan.Abstractions;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class ShahrestanRepository(AccreditationDbContext context) : IShahrestanRepository
{
    public async Task<List<SelectListCountryDevisionResponse>> GetSelectListSharestanAsync(int ostanId, CancellationToken cancellationToken = default)
    {
        return await context.Shahrestans
                 .Where(x => x.OstanId == ostanId &&  !x.IsDeleted)
                 .Select(x => new SelectListCountryDevisionResponse(x.Id, x.Title))
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
    }
}