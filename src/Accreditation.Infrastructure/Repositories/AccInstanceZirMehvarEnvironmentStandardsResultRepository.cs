using Accreditation.Application.Common.Interfaces.Persistence.accInstanceZirMehvarEnvironmentStandardsResults;
using Accreditation.Domain.AccInstanceZirMehvarEnvironmentStandardsResults.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class AccInstanceZirMehvarEnvironmentStandardsResultRepository(AccreditationDbContext _context) : IAccInstanceZirMehvarEnvironmentStandardsResultRepository
{


    public void Add(AccInstanceZirMehvarEnvironmentStandardsResult accInstanceMehvarEnvironmentStandardsResult)
    {
        _context.AddAsync(accInstanceMehvarEnvironmentStandardsResult);
    }

    public async Task<List<AccInstanceZirMehvarEnvironmentStandardsResult>> GetListAccInstanceZirMehvarAsync(Guid accInstanceZirMehvarGuid)
    {
        return _context.AccInstanceZirMehvarEnvironmentStandardsResults
                        .Where(x => x.AccInstanceZirMehvarGUID == accInstanceZirMehvarGuid)
                        .AsNoTracking()
                        .ToList();
    }
    public void Delete(AccInstanceZirMehvarEnvironmentStandardsResult accInstanceMehvarEnvironmentStandardsResult)
    {
        _context.AccInstanceZirMehvarEnvironmentStandardsResults.Remove(accInstanceMehvarEnvironmentStandardsResult);
    }
}
