using Accreditation.Application.Abstractions.Messaging;
using SharedKernel;

namespace Accreditation.Application.CountryDivisions.Shahrestan.GetSelectList;

public sealed record GetShahrestanSelectListQuery(int ostanId) : IQuery<List<SelectListCountryDevisionResponse>>;
