using Accreditation.Application.Abstractions.Messaging;
using SharedKernel;

namespace Accreditation.Application.CountryDivisions.Shahr.GetSelectList
{
    public sealed record GetShahrByBakhshSelectListQuery(int BakhshId) : IQuery<List<SelectListCountryDevisionResponse>>;
}
