using Accreditation.Application.Universityes.GetAll;
using SharedKernel;

namespace Accreditation.Application.Common.Interfaces.Persistence.Universityes
{
    public interface IUniversityRepository
    {
        Task<List<GetAllUniversityDto>> GetUniversity(GetAllUniversityQuery query, CancellationToken cancellationToken);
        Task<List<SelectListResponse>> GetSelectListUniversityAsync(CancellationToken cancellationToken = default);
    }
}
