using Accreditation.Application.Abstractions.Messaging;
using SharedKernel;

namespace Accreditation.Application.Universityes.GetSelectList;

public sealed record GetUniversitySelectListQuery() : IQuery<List<SelectListResponse>>;

