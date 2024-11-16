using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.SanjehEnvironmentStandards.GetList
{
    public sealed record GetListSanjehEnvironmentStandardBySanjehIdQuery(Guid SanjehGuid) :
IQuery<List<GetListSanjehEnvironmentStandardBySanjehIdDto>>;
}
