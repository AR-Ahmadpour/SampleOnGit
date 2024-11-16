using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Mehvars.Dtos;


namespace Accreditation.Application.Mehvars.GetList
{
    public sealed record GetListOfMehvarQuery(Guid Etebardorehid) :
    IQuery<List<GetListOfMehvarsByEtebarDorehIdDto>>;
}
