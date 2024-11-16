using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Shahr.GetList
{
    public sealed record GetShahrByOstandIdQuery(int ostanId) :
IQuery<List<GetListByOstandIdDto>>;

}
