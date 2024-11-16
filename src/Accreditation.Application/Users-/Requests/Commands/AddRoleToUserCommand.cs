using MediatR;
using Accrediation.Application.Services.UserServices.Dtos;

namespace Accrediation.Application.Users.Requests.Commands
{
    public class AddRoleToUserCommand : IRequest<string>
    {
        public AddUserToRoleDto AddUserToRoleDto { get; set; }
    }
}
