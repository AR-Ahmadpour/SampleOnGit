using Accreditation.Application.Common.Interfaces.Persistence.accInstanceStandardEnvironmentStandardsResults;
using Accreditation.Domain.AccInstanceStandardEnvironmentStandardsResults.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class AccInstanceStandardEnvironmentStandardsResultRepository(AccreditationDbContext _context) : IAccInstanceStandardEnvironmentStandardsResultRepository
{
    public void Add(AccInstanceStandardEnvironmentStandardsResult accInstanceStandardResult)
    {
        _context.AddAsync(accInstanceStandardResult);
    }

    public void Delete(AccInstanceStandardEnvironmentStandardsResult accInstanceStandardResult)
    {
        _context.Remove(accInstanceStandardResult);
    }

    public async Task<AccInstanceStandardEnvironmentStandardsResult?> FindByAccStandardGuidAsync(Guid accStandardGuid)
    {
        return await _context.AccInstanceStandardEnvironmentStandardsResults.FirstOrDefaultAsync(x => x.AccInstanceStandardGUID == accStandardGuid);
    }

    public async Task<List<AccInstanceStandardEnvironmentStandardsResult>> GetListByAccInstanceStandardAsync(Guid accInstanceStandardGuid)
    {
        return await _context.AccInstanceStandardEnvironmentStandardsResults
                   .Where(x => x.AccInstanceStandardGUID == accInstanceStandardGuid)
                   .AsNoTracking()
                   .ToListAsync();
    }
}