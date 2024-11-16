using Accreditation.Application.Common.Interfaces.Persistence.SanjehEnvironmentStandards;
using Accreditation.Application.SanjehEnvironmentStandards.GetList;
using Accreditation.Domain.NotNaSanjehs.Entities;
using Accreditation.Domain.SanjeEnvironemtnStandards.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class SanjehEnvironmentStandardRepository(AccreditationDbContext context) : ISanjehEnvironmentStandardRepository
{
    public void Add(SanjehEnvironmentStandard sanjehEnvironmentStandard)
    {
        context.SanjehEnvironmentStandard.Add(sanjehEnvironmentStandard);
    }

    public async Task<List<GetListSanjehEnvironmentStandardDto>> GetAllSanjehEnvironmentStandard(CancellationToken cancellationToken = default)
    {
        return await context.SanjehEnvironmentStandard
            .Select(s => new GetListSanjehEnvironmentStandardDto
            {
                Guid = s.GUID,
                SanjehGuid = s.SanjehGUID,
                EnvironmentStandardGuid = s.EnvironmentStandardGUID

            })
            .ToListAsync(cancellationToken);
    }

    public async Task<List<GetListSanjehEnvironmentStandardBySanjehIdDto>> GetAllSanjehEnvironmentStandardBySanjehId(Guid SanjehId,CancellationToken cancellationToken = default)
    {
        return await context.SanjehEnvironmentStandard
            .Where(x => x.SanjehGUID == SanjehId)
            .Select(s => new GetListSanjehEnvironmentStandardBySanjehIdDto
            {
                Guid = s.EnvironmentStandardGUID,
                title = s.EnvironmentStandard.Title
            }).ToListAsync();
    }

    public async Task<List<SanjehEnvironmentStandard>> GetBySanjehGuidAsync(Guid sanjehGuid, CancellationToken cancellationToken)
    {
        return await context.Set<SanjehEnvironmentStandard>()
                     .Where(n => n.SanjehGUID == sanjehGuid)
                     .ToListAsync(cancellationToken);
    }

    public void RemoveRange(IEnumerable<SanjehEnvironmentStandard> sanjehEnvironmentStandards)
    {
        context.Set<SanjehEnvironmentStandard>().RemoveRange(sanjehEnvironmentStandards);
    }
}
