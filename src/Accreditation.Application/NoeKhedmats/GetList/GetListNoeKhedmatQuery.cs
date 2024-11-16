using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.NoeKhedmats.GetList
{
    public sealed record GetListNoeKhedmatQuery() :
        IQuery<List<GetListNoeKhedmatDto>>;
}
