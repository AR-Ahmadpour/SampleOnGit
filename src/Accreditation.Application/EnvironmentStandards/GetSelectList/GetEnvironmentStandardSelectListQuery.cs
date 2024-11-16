using Accreditation.Application.Abstractions.Messaging;
using SharedKernel;

namespace Accreditation.Application.EnvironmentStandards.GetSelectList;

public sealed record GetEnvironmentStandardSelectListQuery() : 
    IQuery<List<SelectListResponse>>;
