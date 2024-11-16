using Accreditation.Application.InstanceType.Getlist;

namespace Accreditation.Application.Common.Interfaces.Persistence.InstanceType;
public interface IInstanceTypeRepository
{
    Task<List<InstanceTypeDto>> GetSelectListInstanceTypeAsync(CancellationToken cancellationToken = default);
}
