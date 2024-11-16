using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Sanjehs.GetById;

namespace Accreditation.Application.UserInfos.GetById
{
    public sealed record GetUserInfoByUserGuidQuery(Guid UserGuid) :
 IQuery<GetUserInfoByUserGuidDto>;
}
