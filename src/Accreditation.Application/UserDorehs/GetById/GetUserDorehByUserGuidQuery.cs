using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Tahsilats.GetById;

namespace Accreditation.Application.UserDorehs.GetById
{
    public sealed record GetUserDorehByUserGuidQuery(Guid UserGuid) :
IQuery<List<GetUserDorehByUserGuidDto>>;
}
