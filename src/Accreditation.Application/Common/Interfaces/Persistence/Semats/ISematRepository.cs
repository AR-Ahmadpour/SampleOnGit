using Accreditation.Application.Semats.GetList;

namespace Accreditation.Application.Common.Interfaces.Persistence.Semats
{
    public interface ISematRepository
    {
        Task<List<GetListSematDto>> GetListAsync(CancellationToken cancellationToken = default);
    }
}
