using Accreditation.Domain.SanjehLevels.Abstractions;
using Accreditation.Domain.SanjehLevels.Dtos;
using Accreditation.Domain.SanjehLevels.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class SanjehLevelRepository(AccreditationDbContext context) : ISanjehLevelRepository
{
    public async Task<SanjehLevel?> FindAsync(int SanjehLevelId, CancellationToken cancellationToken = default)
    {
        return await context.SanjehLevels.FindAsync(SanjehLevelId, cancellationToken);
    }

    public async Task<List<GetAllSanjehLevelDto>> GetAllSanjehLevel(CancellationToken cancellationToken = default)
    {
        return await context.SanjehLevels
        .Select(s => new GetAllSanjehLevelDto
        {
            Id = s.Id,
            Guid = s.GUID,
            Title = s.Title,
        })
        .AsNoTracking()
        .ToListAsync(cancellationToken);
    }
}
