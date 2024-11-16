

using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;

namespace Accreditation.Application.Users.ToggleUserPermission
{
    public sealed record ToggleUserPermissionCommand(int UserPermissionID,Guid CurrentUserId) : ICommand;
}
