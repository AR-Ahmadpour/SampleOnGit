using Accreditation.Application.Abstractions.Messaging;
using SharedKernel;

namespace Accreditation.Application.OrgGerayesh.GetSelectList;

public sealed record GetOrgGerayeshSelectListQuery(Guid orgTypeGuid) : IQuery<List<SelectListResponse>>;
