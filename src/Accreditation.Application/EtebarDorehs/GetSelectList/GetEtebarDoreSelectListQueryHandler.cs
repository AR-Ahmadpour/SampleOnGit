using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.EtebarDorehs.GetSelectList;
using SharedKernel;

namespace Accreditation.Application.OrgTypes.GetSelectList;

internal sealed class GetEtebarDoreSelectListQueryHandler
: IQueryHandler<GetEtebarDoreSelectListQuery, List<GetEtebarDoreSelectListDto>>
{
    private readonly IEtebarDorehRepository _etebarDorehRepository;

    public GetEtebarDoreSelectListQueryHandler(IEtebarDorehRepository etebarDorehRepository)
    {
        _etebarDorehRepository = etebarDorehRepository;
    }
    public async Task<Result<List<GetEtebarDoreSelectListDto>>> Handle(GetEtebarDoreSelectListQuery query, CancellationToken cancellationToken)
    {
        var etebarDorehList = await _etebarDorehRepository.GetSelectListByOrgTypeIdAsync(query.Guid, cancellationToken);

        return etebarDorehList;
    }
}
