using Accrediation.Application.Authentication.Requests.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Accrediation.Application.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<string> Register(RegisterCommand command, CancellationToken cancellationToken);

        Task<string> Login(LoginCommand request);
    }
}
