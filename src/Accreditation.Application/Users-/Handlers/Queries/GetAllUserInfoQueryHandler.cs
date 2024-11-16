using Accrediation.Application.Services.UserServices.Dtos;
using Accrediation.Application.Users.Requests.Queries;
using Accrediation.Application.Services.UserServices;
using MediatR;
using Accrediation.Application.Common.Models;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Services.UserServices.Dtos;

namespace Accrediation.Application.Users.Handlers.Queries
{
    public class GetAllUserInfoQueryHandler 
        : IRequestHandler<GetAllUsersInfoQuery, PagedList<GetAllUsersInfoDto>>
    {
        private readonly IUserService _userService;

        public GetAllUserInfoQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<PagedList<GetAllUsersInfoDto>> Handle(
            GetAllUsersInfoQuery request,
            CancellationToken cancellationToken)
        {
            return await _userService.GetAllUsersInfo(request, cancellationToken);
        }
    }
}
