using MediatR;
using Accrediation.Application.Services.UserServices;
using Accrediation.Application.Users.Requests.Commands;

namespace Accrediation.Application.Users.Handlers.Commands
{
    public class AddRoleToUserCommandHandler 
        : IRequestHandler<AddRoleToUserCommand, string>
    {
        private readonly IUserService _userService;

        public AddRoleToUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Handle(
            AddRoleToUserCommand request,
            CancellationToken cancellationToken)
        {
            return await _userService.AddRoleToUser(request, cancellationToken);
        }
    }
}
