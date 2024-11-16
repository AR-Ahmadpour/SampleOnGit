

using Accreditation.Application.ReshtehMaghtas.GetList;

namespace Accreditation.Application.Common.Interfaces.Persistence.ReshtehMaghta
{
    public interface IReshtehMaghtaRepository
    {
        Task<List<GetListReshtehMaghtaQueryDto>> GetReshtehsByMaghtaIdAsync(Guid maghtaGuid, CancellationToken cancellationToken = default);
    }
}
