using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Semats.GetList
{
    public sealed record GetListSematQuery() : IQuery<List<GetListSematDto>>;
}
