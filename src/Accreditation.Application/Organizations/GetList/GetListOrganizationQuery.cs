using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Models;

namespace Accreditation.Application.Organization.GetList;

public sealed record GetListOrganizationQuery(
    int PageNumber,
    int PageSize,
    Guid? OrgTypeGuid,
    Guid? OrgGerayeshGuid,
    int? OstanId,
    int? ShahrestanId,
    int? BakhshLocationId,
    int? ShahrId,
    int? UnivaersityId,
    string? OrganizationName) :
    IQuery<PagedList<GetListOrganizationDto>>;
