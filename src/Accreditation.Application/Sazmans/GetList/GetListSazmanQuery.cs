using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Sazmans.GetList
{
    public sealed record GetListSazmanQuery() :
IQuery<List<GetListSazmanDto>>;
}
