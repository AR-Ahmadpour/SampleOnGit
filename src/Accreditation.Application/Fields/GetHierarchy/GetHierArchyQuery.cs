using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Fields.GetHierarchy
{
    public sealed record GetListHierArchyQuery(Guid mehvarId) :
IQuery<List<HierarchyNodeDto>>;
}
