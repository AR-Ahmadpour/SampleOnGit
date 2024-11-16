using Accrediation.Application.Services.AuthenticationServices.Dtos;
using MediatR;

namespace Accrediation.Application.Users.Requests.Queries
{
    public class GetUserShowInfoQuery : IRequest<GetUserInfoDto>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
