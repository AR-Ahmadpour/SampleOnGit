using Accrediation.Application.Common.Models;
using Accrediation.Application.Services.UserServices;
using Accrediation.Application.Services.UserServices.Dtos;
using Accrediation.Application.Users.Requests.Queries;
using MediatR;

namespace Accrediation.Application.Users.Handlers.Queries
{
    public class GetUserSearchHandher : IRequestHandler<GetSearchQuery, PagedList<GetAllUsersInfoDto>>
    {
        private readonly IUserService _userService;

        public GetUserSearchHandher(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<PagedList<GetAllUsersInfoDto>> Handle(
        GetSearchQuery request,
        CancellationToken cancellationToken)
        {
            return await _userService.GetUserSearch(request, cancellationToken);
        }
    }
}
