using Accreditation.Application.Sanjehs.GetList;
using Accreditation.Application.Sazmans.GetList;

namespace Accreditation.Application.Common.Interfaces.Persistence.Sazmans
{
    public interface ISazmanRepository
    {
        Task<List<GetListSazmanDto>> GetListAsync(CancellationToken cancellationToken = default);
    }
}
