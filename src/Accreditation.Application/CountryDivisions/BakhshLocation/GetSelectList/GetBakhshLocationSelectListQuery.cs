using Accreditation.Application.Abstractions.Messaging;
using SharedKernel;

namespace Accreditation.Application.CountryDivisions.BakhshLocation.GetSelectList;

public sealed record GetBakhshLocationSelectListQuery(int shahrestanId) : IQuery<List<SelectListCountryDevisionResponse>>;
