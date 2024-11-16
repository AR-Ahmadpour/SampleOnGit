using Accreditation.Application.SanjehEnvironmentStandards.GetList;
using Accreditation.Domain.NotNaSanjehs.Entities;
using Accreditation.Domain.SanjeEnvironemtnStandards.Entities;


namespace Accreditation.Application.Common.Interfaces.Persistence.SanjehEnvironmentStandards
{
    public interface ISanjehEnvironmentStandardRepository
    {
        Task<List<GetListSanjehEnvironmentStandardDto>> GetAllSanjehEnvironmentStandard(CancellationToken cancellationToken = default);
        Task<List<GetListSanjehEnvironmentStandardBySanjehIdDto>> GetAllSanjehEnvironmentStandardBySanjehId(Guid SanjehId, CancellationToken cancellationToken = default);
        void Add(SanjehEnvironmentStandard sanjehEnvironmentStandard);
        Task<List<SanjehEnvironmentStandard>> GetBySanjehGuidAsync(Guid sanjehGuid, CancellationToken cancellationToken);
        void RemoveRange(IEnumerable<SanjehEnvironmentStandard> sanjehEnvironmentStandards);

    }
}
