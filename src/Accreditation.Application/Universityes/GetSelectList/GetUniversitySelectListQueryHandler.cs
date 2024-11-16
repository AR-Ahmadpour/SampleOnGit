using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Universityes;
using SharedKernel;

namespace Accreditation.Application.Universityes.GetSelectList;

internal sealed class GetUniversitySelectListQueryHandler(IUniversityRepository universityRepository) : IQueryHandler<GetUniversitySelectListQuery, List<SelectListResponse>>
{
    public async Task<Result<List<SelectListResponse>>> Handle(GetUniversitySelectListQuery request,
                                                         CancellationToken cancellationToken)
    {
        return await universityRepository.GetSelectListUniversityAsync(cancellationToken);
    }
}
