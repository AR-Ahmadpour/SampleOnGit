using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstanceStatusTypes;
using Accreditation.Domain.AccreditationInstanceStatusTypes.Entities;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class AccreditationInstanceStatusTypeRepository(AccreditationDbContext context) : IAccreditationInstanceStatusTypeRepository
{
    public async Task<AccreditationInstanceStatusType?> FindAsyc(int id, CancellationToken cancellationToken)
    {
        return await context.AccreditationInstanceStatusTypes.FindAsync(id, cancellationToken);
    }

    public async Task<AccreditationInstanceStatusType?> FindBasedInstancetypeIdAsyc(int instanceTypeId, int stepOrder, CancellationToken cancellationToken)
    {
        return await context.AccreditationInstanceStatusTypes.FirstOrDefaultAsync(x => x.InstanceTypeId == instanceTypeId && x.StepOrder == stepOrder);
    }
}