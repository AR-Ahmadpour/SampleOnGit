using Accreditation.Domain.CountryDivisions.Ostans.Entities;
using SharedKernel;



public interface IOstanRepository
{
    Task<List<SelectListCountryDevisionResponse>> GetSelectListByOstanIdAsync(CancellationToken cancellationToken = default);
    Task<Ostan?> FindAsync(int OstanId, CancellationToken cancellationToken = default);
}


