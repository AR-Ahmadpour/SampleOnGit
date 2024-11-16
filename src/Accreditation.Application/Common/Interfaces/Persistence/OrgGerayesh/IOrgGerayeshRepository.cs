using SharedKernel;

namespace Accreditation.Domain.OrgGerayesh.Abstractions;

public interface IOrgGerayeshRepository
{
    Task<List<SelectListResponse>> GetSelectListOrgGerayeshAsync(Guid orgTypeGuid,CancellationToken cancellationToken = default);
    Task<bool> FindAsync(Guid guid, CancellationToken cancellationToken = default);
}
