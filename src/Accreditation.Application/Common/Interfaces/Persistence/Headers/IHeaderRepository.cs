using Accreditation.Application.Headers.GetList;

namespace Accreditation.Application.Common.Interfaces.Persistence.Headers
{
    public interface IHeaderRepository
    {
        Task<GetHeaderDto> GetHeader(Guid fieldGuid, Guid accreditationInstanceGuid,CancellationToken cancellationToken);
    }
}
