using Accreditation.Domain.AccreditationInstanceStatuses;

namespace Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstanceStatuses;
public interface IAccreditationInstanceStatusRepository
{
    void Add(AccreditationInstanceStatus accreditationInstanceStatu);
    void Delete(AccreditationInstanceStatus accreditationInstanceStatus);
    Task<AccreditationInstanceStatus> FindBasedAccInstanceAsyc(Guid AccreditationInstanceGuid, CancellationToken cancellationToken);
}
