using Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards;
using Accreditation.Domain.EnvironmentStandards.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;


namespace Accreditation.Infrastructure.Repositories;

internal sealed class EnvironmentStandardRepository(AccreditationDbContext context) : IEnvironmentStandardRepository
{
    public async Task<bool> FindAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var exists = await context.EnvironmentStandards
      .AnyAsync(x => x.GUID == guid, cancellationToken);

        return exists;
    }

    public async Task<List<GetListByEtebarDorehDto>> GetListByEtebarDorehIdAsync(Guid etebarDorehGuid, CancellationToken cancellationToken = default)
    {
        return await context.EnvironmentStandards
        .Where(x => x.EtebarDorehGUID == etebarDorehGuid)
        .Select(x => new GetListByEtebarDorehDto(x.GUID,x.Title,x.IsDeleted))
        .ToListAsync();
     

    }

    public async Task<List<SelectListResponse>> GetSelectList(CancellationToken cancellationToken = default)
    {
        return await context.EnvironmentStandards
            .Select(s => new SelectListResponse(s.GUID, s.Title))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<SelectListResponse>> GetSelectListAsync(Guid etebarDoreGuid)
    {
        return await context.EnvironmentStandards
                     .Where(x =>  x.EtebarDorehGUID == etebarDoreGuid &&
                                  !x.IsDeleted)
                      .Select(s => new SelectListResponse(s.GUID, s.Title))
                     .ToListAsync();
    }
    public List<Guid> GetGuidList(Guid etebarDoreGuid)
    {
        return   context.EnvironmentStandards
                     .Where(x => x.EtebarDorehGUID == etebarDoreGuid && !x.IsDeleted)
                     .Select(s => s.GUID) 
                     .ToList();
    }
}
