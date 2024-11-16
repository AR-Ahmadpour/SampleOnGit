using SharedKernel;

namespace Accreditation.Domain.CountryDivisions.Shahr.Abstractions
{
    public interface IShahrRepository
    {
        Task<List<SelectListCountryDevisionResponse>> GetSelectListSharAsync(int OstanId, CancellationToken cancellationToken = default);
        Task<List<SelectListCountryDevisionResponse>> GetSelectListSharByBakhshAsync(int BakhshId, CancellationToken cancellationToken = default);
    }
}
