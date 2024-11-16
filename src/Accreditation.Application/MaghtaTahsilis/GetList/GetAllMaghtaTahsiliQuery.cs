using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.MaghtaTahsilis.GetList
{
    public sealed record GetListMaghtaTahsiliQuery() :
IQuery<List<GetAllMaghtaTahsiliQueryDto>>;
}
