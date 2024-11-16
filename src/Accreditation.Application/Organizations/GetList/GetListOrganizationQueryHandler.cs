using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Organization;
using Accreditation.Application.Common.Models;
using SharedKernel;

namespace Accreditation.Application.Organization.GetList;

internal sealed class GetListOrganizationQueryHandler
: IQueryHandler<GetListOrganizationQuery, PagedList<GetListOrganizationDto>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetListOrganizationQueryHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Result<PagedList<GetListOrganizationDto>>> Handle(
        GetListOrganizationQuery request,
        CancellationToken cancellationToken)
    {
        return await _organizationRepository.GetSelectListOrganizationAsync(
            request.OrgTypeGuid, request.OrgGerayeshGuid,
            request.OstanId, request.ShahrestanId,
            request.BakhshLocationId, request.ShahrId,
            request.UnivaersityId, request.OrganizationName,
            request.PageNumber, request.PageSize,
            cancellationToken = default);
    }
}
