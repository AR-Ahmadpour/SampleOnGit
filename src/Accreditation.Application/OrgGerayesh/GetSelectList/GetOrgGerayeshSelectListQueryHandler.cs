using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.OrgGerayesh.Abstractions;
using SharedKernel;

namespace Accreditation.Application.OrgGerayesh.GetSelectList;

internal sealed class GetOrgGerayeshSelectListQueryHandler :
                 IQueryHandler<GetOrgGerayeshSelectListQuery, List<SelectListResponse>>
{
    private readonly IOrgGerayeshRepository _orgGerayeshRepository;

    public GetOrgGerayeshSelectListQueryHandler(IOrgGerayeshRepository orgGerayeshRepository)
    {
        _orgGerayeshRepository = orgGerayeshRepository;
    }
    public async Task<Result<List<SelectListResponse>>> Handle(GetOrgGerayeshSelectListQuery request,
                                                         CancellationToken cancellationToken)
    {
        return await _orgGerayeshRepository.GetSelectListOrgGerayeshAsync(request.orgTypeGuid, cancellationToken);
    }
}