using Accrediation.Application.Services.AuthenticationServices;
using Accrediation.Application.Authentication.Requests.Commands;
using MediatR;

namespace Accrediation.Application.Authentication.Handlers.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginCommandHandler(
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<string> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            return await _authenticationService.Login(request);
        }
    }
}
