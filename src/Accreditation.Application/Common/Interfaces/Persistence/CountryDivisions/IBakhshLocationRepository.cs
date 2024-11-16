using Accreditation.Domain.CountryDivisions.BakhshLocation.Entities;
using SharedKernel;

namespace Accreditation.Domain.CountryDivisions.BakhshLocation.Abstractions
{
    public interface IBakhshLocationRepository
    {
        Task<List<SelectListCountryDevisionResponse>> GetSelectListBakhshLocationAsync(int shahrestanId, CancellationToken cancellationToken = default);
        Task<Bakhsh?> FindAsync(int BakhshId, CancellationToken cancellationToken = default);
    }
}
