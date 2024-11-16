using Accreditation.Application.Common.Interfaces.Persistence.accInstanceMehvarEnvironmentStandardsResults;
using Accreditation.Domain.AccInstanceMehvarEnvironmentStandardsResults.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class AccInstanceMehvarEnvironmentStandardsResultRepository(AccreditationDbContext _context) : IAccInstanceMehvarEnvironmentStandardsResultRepository
{


    public void Add(AccInstanceMehvarEnvironmentStandardsResult accInstanceMehvarEnvironmentStandardsResult)
    {
        _context.AddAsync(accInstanceMehvarEnvironmentStandardsResult);
    }

    public async Task<List<AccInstanceMehvarEnvironmentStandardsResult>> GetListAccInstanceMehvarAsync(Guid accInstanceMehvarGUID)
    {
        return await _context.AccInstanceMehvarEnvironmentStandardsResults
                        .Where(x => x.AccInstanceMehvarGUID == accInstanceMehvarGUID)
                        .AsNoTracking()
                       .ToListAsync();
    }
    public void Delete(AccInstanceMehvarEnvironmentStandardsResult accInstanceMehvarEnvironmentStandardsResult)
    {
        _context.AccInstanceMehvarEnvironmentStandardsResults.Remove(accInstanceMehvarEnvironmentStandardsResult);
    }
}
