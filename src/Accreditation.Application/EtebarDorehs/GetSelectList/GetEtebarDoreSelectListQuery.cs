using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.EtebarDorehs.GetSelectList;

namespace Accreditation.Application.OrgTypes.GetSelectList;

public sealed record GetEtebarDoreSelectListQuery(Guid Guid) : IQuery<List<GetEtebarDoreSelectListDto>>;

