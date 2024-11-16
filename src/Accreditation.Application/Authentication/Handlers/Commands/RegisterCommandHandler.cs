using Accrediation.Application.Authentication.Requests.Commands;
using Accrediation.Application.Services.AuthenticationServices;
using MediatR;

namespace Accrediation.Application.Authentication.Handlers.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IAuthenticationService _authenticationService;

        public RegisterCommandHandler(
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<string> Handle(
            RegisterCommand command,
            CancellationToken cancellationToken)
        {
            return await _authenticationService.Register(command, cancellationToken);
        }
    }
}
