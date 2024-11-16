using Accrediation.Application.Services.AuthenticationServices.Dtos;
using Accrediation.Application.Users.Requests.Queries;
using Accrediation.Application.Services.UserServices;
using MediatR;

namespace Accrediation.Application.Users.Handlers.Queries
{
    public class GetUserInfoRequestHandler 
        : IRequestHandler<GetUserInfoQuery, GetUserInfoDto>
    {
        private readonly IUserService _userService;

        public GetUserInfoRequestHandler(
            IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserInfoDto> Handle(
            GetUserInfoQuery request,
            CancellationToken cancellationToken)
        {
            return await _userService.GetUserInfo(request, cancellationToken);
        }
    }
}
