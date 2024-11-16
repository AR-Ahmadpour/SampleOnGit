using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstanceStatuses;
using Accreditation.Domain.AccreditationInstanceStatuses;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure.Repositories;

internal sealed class AccreditationInstanceStatusRepository(AccreditationDbContext context) : IAccreditationInstanceStatusRepository
{
    public void Add(AccreditationInstanceStatus accreditationInstanceStatus)
    {
        context.AccreditationInstanceStatuses.Add(accreditationInstanceStatus);
    }
    public void Delete(AccreditationInstanceStatus accreditationInstanceStatus)
    {
        context.AccreditationInstanceStatuses.Remove(accreditationInstanceStatus);
    }
    public async Task<AccreditationInstanceStatus?> FindBasedAccInstanceAsyc(Guid AccreditationInstanceGuid, CancellationToken cancellationToken)
    {
        return await context.AccreditationInstanceStatuses.FirstOrDefaultAsync(x => x.AccreditationInstanceGUID == AccreditationInstanceGuid, cancellationToken);
    }
}
