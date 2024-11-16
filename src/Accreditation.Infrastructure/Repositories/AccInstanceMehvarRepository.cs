using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceMehvars;
using Accreditation.Domain.AccInstanceMehvars.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class AccInstanceMehvarRepository : IAccInstanceMehvarRepository
{
    private readonly AccreditationDbContext _context;
    private readonly ILogger<AccInstanceMehvarRepository> _logger;

    public AccInstanceMehvarRepository(AccreditationDbContext context,
                                       ILogger<AccInstanceMehvarRepository> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task Add(AccInstanceMehvar accInstanceMehvar)
    {
        try
        {
            await _context.AddAsync(accInstanceMehvar);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to create AccreditationInstance for EvaluationId: {accInstanceMehvar.AccreditationInstanceGUID}");
            throw;
        }
    }

    public async Task<List<AccInstanceMehvar>> GetListAccInstanceMehvarAsync(Guid accInstanceGuid)
    {
        return await _context.AccInstanceMehvars
                        .Where(x => x.AccreditationInstanceGUID == accInstanceGuid)
                        .AsNoTracking()
                       .ToListAsync();
    }
    public void Delete(AccInstanceMehvar accInstanceMehvar)
    {
        _context.AccInstanceMehvars.Remove(accInstanceMehvar);
    }

    public async Task<AccInstanceMehvar?> Find(Guid Id)
    {
        return await _context.AccInstanceMehvars.FindAsync( Id);
    }
}
