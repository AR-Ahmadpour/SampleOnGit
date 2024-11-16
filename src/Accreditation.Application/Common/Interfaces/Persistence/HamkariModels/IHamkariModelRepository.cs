using Accreditation.Application.HamkariModels.GetList;

namespace Accreditation.Application.Common.Interfaces.Persistence.HamkariModels
{
    public interface IHamkariModelRepository
    {
        Task<List<GetListHamkariModelDto>> GetListAsync(CancellationToken cancellationToken = default);
    }
}
