using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.SanjehEnvironmentStandards.GetList
{
    public sealed record GetListSanjehEnvironmentStandardQuery() :
 IQuery<List<GetListSanjehEnvironmentStandardDto>>;
}
