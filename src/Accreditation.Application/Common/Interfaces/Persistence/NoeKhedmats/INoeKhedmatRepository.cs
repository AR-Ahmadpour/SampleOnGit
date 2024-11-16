using Accreditation.Application.NoeKhedmats.GetList;

namespace Accreditation.Application.Common.Interfaces.Persistence.NoeKhedmats
{
    public interface INoeKhedmatRepository
    {
        Task<List<GetListNoeKhedmatDto>> GetListAsync(CancellationToken cancellationToken = default);
    }
}
