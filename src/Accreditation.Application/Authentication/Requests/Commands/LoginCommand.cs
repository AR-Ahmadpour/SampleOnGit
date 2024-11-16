using Accrediation.Application.Services.AuthenticationServices.Dtos;
using MediatR;

namespace Accrediation.Application.Authentication.Requests.Commands
{
    public class LoginCommand : IRequest<string>
    {
        public LoginDto LoginDto { get; set; }
    }
}
