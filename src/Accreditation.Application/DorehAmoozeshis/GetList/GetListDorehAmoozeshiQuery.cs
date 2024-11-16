using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.DorehAmoozeshis.GetList
{
    public sealed record GetListDorehAmoozeshiQuery() :
IQuery<List<GetListDorehAmoozeshiDto>>;
}
