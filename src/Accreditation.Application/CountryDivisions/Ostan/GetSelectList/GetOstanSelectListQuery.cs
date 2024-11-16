using Accreditation.Application.Abstractions.Messaging;
using SharedKernel;

namespace Accreditation.Application.CountryDivisions.Ostan.GetSelectList;

public sealed record GetOstanSelectListQuery() : IQuery<List<SelectListCountryDevisionResponse>>;
