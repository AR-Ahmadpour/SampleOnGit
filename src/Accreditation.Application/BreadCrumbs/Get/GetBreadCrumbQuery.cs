using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.BreadCrumbs.Get
{
    public sealed record GetBreadCrumbQuery(Guid? Guid, string? GuidType) :
    IQuery<List<GetBreadCrumbDto>>;
}
