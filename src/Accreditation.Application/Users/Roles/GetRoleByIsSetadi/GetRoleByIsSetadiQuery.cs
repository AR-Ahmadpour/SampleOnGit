using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Users.Roles.GetRoleByIsSetadi
{
    public sealed record GetRoleByIsSetadiQuery(bool IsSetadi) : IQuery<List<GetRoleByIsSetadiDto>>;
}
