using Accreditation.Domain.AccreditationInstanceStatusTypes.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstanceStatusTypes;

public interface IAccreditationInstanceStatusTypeRepository
{
    Task<AccreditationInstanceStatusType> FindAsyc(int id, CancellationToken cancellationToken);
    Task<AccreditationInstanceStatusType> FindBasedInstancetypeIdAsyc(int instanceTypeId, int stepOrder, CancellationToken cancellationToken);
}
