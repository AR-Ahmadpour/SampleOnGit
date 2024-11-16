using Accrediation.Application.Services.UserServices.Dtos;
using Accrediation.Application.Users.Requests.Queries;
using Accrediation.Application.Services.UserServices;
using MediatR;
using Accreditation.Application.Services.UserServices.Dtos;

namespace Accrediation.Application.Users.Handlers.Queries
{
    public class GetAllRoleQueryHandler 
        : IRequestHandler<GetAllRoleQuery, List<GetAllRoleDto>>
    {
        private readonly IUserService _userService;
        public GetAllRoleQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<GetAllRoleDto>> Handle(
            GetAllRoleQuery request,
            CancellationToken cancellationToken)
        {
            return await _userService.GetAllRole(cancellationToken);
        }
    }
}
