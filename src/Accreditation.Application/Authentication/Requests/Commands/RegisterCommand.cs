using MediatR;
using Accrediation.Application.Services.AuthenticationServices.Dtos;

namespace Accrediation.Application.Authentication.Requests.Commands
{
    public class RegisterCommand: IRequest<string>
    {
        public RegisterDto RegisterDto { get; set; }
    }
}
