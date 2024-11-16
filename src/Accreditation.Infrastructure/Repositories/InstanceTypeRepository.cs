using Accreditation.Application.Common.Interfaces.Persistence.InstanceType;
using Accreditation.Application.InstanceType.Getlist;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
namespace Accreditation.Infrastructure.Repositories;

internal sealed class InstanceTypeRepository(AccreditationDbContext context) : IInstanceTypeRepository
{

    public async Task<List<InstanceTypeDto>> GetSelectListInstanceTypeAsync(CancellationToken cancellationToken = default)
    {
        return await context.InstanceTypes
               .Where(x => x.IsActive)
               .Select(x => new InstanceTypeDto
               {
                   Id = x.Id,
                   Title = x.Title,
                   IsActive = x.IsActive,
                   IsActiveInStaff = x.IsActiveInStaff,
                   IsActiveInUniversity = x.IsActiveInUniversity
               })
               .AsNoTracking()
               .ToListAsync(cancellationToken);
    }
}