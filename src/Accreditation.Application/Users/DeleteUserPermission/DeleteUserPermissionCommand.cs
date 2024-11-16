
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;

namespace Accreditation.Application.Users.DeleteUserPermission
{
    public sealed record DeleteUserPermissionCommand(int UserPermissionID) : ICommand;
}
