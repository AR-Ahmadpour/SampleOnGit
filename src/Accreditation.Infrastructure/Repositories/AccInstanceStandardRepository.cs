using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceStandards;
using Accreditation.Domain.AccInstanceStandards.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class AccInstanceStandardRepository : IAccInstanceStandardRepository
{
    private readonly AccreditationDbContext _context;

    public AccInstanceStandardRepository(AccreditationDbContext context)
    {
        _context = context;
    }


    public async Task Add(AccInstanceStandard accInstanceStandard)
    {
        await _context.AddAsync(accInstanceStandard);
    }

    public void Delete(AccInstanceStandard accInstanceStandard)
    {
        _context.AccInstanceStandards.Remove(accInstanceStandard);
    }

    public async Task<AccInstanceStandard> FindByAccInstanceAndAcczirmehvarGuid(Guid accInstanceGuid, Guid accInsZirMehvarGuid)
    {
        var result = await _context.AccInstanceStandards.Where(x => x.AccInstanceZirMehvarGUID == accInsZirMehvarGuid &&
                                                               x.AccreditationInstanceGUID == accInstanceGuid)
                                                        .AsNoTracking().ToListAsync();
        return result.FirstOrDefault();
    }

    public async Task<List<AccInstanceStandard>> FindByAccInstanceGuid(Guid accInstanceGuid)
    {
        return await _context.AccInstanceStandards.Where(x => x.AccreditationInstanceGUID == accInstanceGuid).AsNoTracking().ToListAsync();
    }
}
