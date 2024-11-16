using Accreditation.Application.MaghtaTahsilis.GetList;
using Accreditation.Application.ReshtehMaghtas.GetList;
using Accreditation.Domain.ReshteMaghtas.Entities;
using Accreditation.Domain.Sanjehs.Entities;

namespace Accreditation.Application.Common.Interfaces.Persistence.MaghtaTahsilis
{
    public interface IMaghtaTahsiliRepository
    {
        Task<List<GetAllMaghtaTahsiliQueryDto>> GetListMaghtaTahsiliAsync(CancellationToken cancellationToken = default);
        Task<bool> FindAsync(Guid guid, CancellationToken cancellationToken = default);
        
    }
}

