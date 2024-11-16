using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstancesEnvironmentStandardsResults;
using Accreditation.Domain.AccreditationInstancesEnvironmentStandardsResults.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class AccreditationInstancesEnvironmentStandardsResultRepository(AccreditationDbContext _context) : IAccreditationInstancesEnvironmentStandardsResultRepository
{
    public void Add(AccreditationInstancesEnvironmentStandardsResult accreditationInstanEnvironmentStandardsResult)
    {
        _context.AddAsync(accreditationInstanEnvironmentStandardsResult);
    }

    public async Task<List<AccreditationInstancesEnvironmentStandardsResult>> GetListAccInstanceAsync(Guid accInstanceGuid)
    {
        return _context.AccreditationInstancesEnvironmentStandardsResults
                        .Where(x => x.AccreditationInstanceGUID == accInstanceGuid)
                        .AsNoTracking()
                        .ToList();
    }
    public void Delete(AccreditationInstancesEnvironmentStandardsResult accInstanceMehvarEnvironmentStandardsResult)
    {
        _context.AccreditationInstancesEnvironmentStandardsResults.Remove(accInstanceMehvarEnvironmentStandardsResult);
    }
}
