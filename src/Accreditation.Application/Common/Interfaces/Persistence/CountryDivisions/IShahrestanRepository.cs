using SharedKernel;

namespace Accreditation.Domain.CountryDivisions.Shahrestan.Abstractions;

public interface IShahrestanRepository
{
    Task<List<SelectListCountryDevisionResponse>> GetSelectListSharestanAsync(int OstanId, CancellationToken cancellationToken = default);
}
