using Accreditation.Application.Common.Interfaces.Persistence.AccInstanceZirMehvars;
using Accreditation.Domain.AccInstanceMehvars.Entities;
using Accreditation.Domain.AccInstanceZirMehvars.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class AccInstanceZirMehvarRepository : IAccInstanceZirMehvarRepository
{
    private readonly AccreditationDbContext _context;
    private readonly ILogger<AccInstanceZirMehvarRepository> _logger;

    public AccInstanceZirMehvarRepository(AccreditationDbContext context,
                                       ILogger<AccInstanceZirMehvarRepository> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task Add(AccInstanceZirMehvar accInstanceZirMehvar)
    {
        try
        {
            await _context.AddAsync(accInstanceZirMehvar);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to create AccInstanceZirMehvar for EvaluationId: {accInstanceZirMehvar.AccreditationInstanceGUID}");
            throw;
        }
    }

    public void Delete(AccInstanceZirMehvar accInstanceMehvar)
    {
        _context.AccInstanceZirMehvars.Remove(accInstanceMehvar);
    }

    public async Task<List<AccInstanceZirMehvar>> GetListAccInstanceZirMehvarAsync(Guid accInstanceGuid)
    {
        return _context.AccInstanceZirMehvars
                .Where(x => x.AccreditationInstanceGUID == accInstanceGuid)
                .AsNoTracking()
                .ToList();
    }
}