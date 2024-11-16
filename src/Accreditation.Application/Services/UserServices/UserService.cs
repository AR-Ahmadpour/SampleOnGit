using Accrediation.Application.Abstractions;
using Accrediation.Application.Abstractions.Persistence.Users;
using Accrediation.Application.Common.Errors.Users;
using Accrediation.Application.Common.Models;
using Accrediation.Application.Services.AuthenticationServices.Dtos;
using Accrediation.Application.Services.UserServices.Dtos;
using Accrediation.Application.Users.Requests.Commands;
using Accrediation.Application.Users.Requests.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Accrediation.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IIdentityService _identityService;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IIdentityService identityService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _identityService = identityService;
        }

        public async Task<string> AddRoleToUser(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AddUserToRoleDto;
            var user = await _identityService.FindById(dto.UserId);

            var role = await _roleRepository.GetRole(dto.RoleId);

            if (role == null)
                throw new RoleNotFoundException();

            if (!await _identityService.IsInRoleAsync(user.Id.ToString(), role.Name, cancellationToken))
                await _identityService.AddToRole(user, role.Name);

            return user.Id.ToString();
        }

        public async Task<PagedList<GetAllUsersInfoDto>> GetAllUsersInfo(
            GetAllUsersInfoQuery request,
            CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllUsersInfo(request, cancellationToken);
        }


        public async Task<GetUserInfoDto> GetUserInfo(
            GetUserInfoQuery request,
            CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserInfo(request, cancellationToken);
        }

        public async Task<List<GetAllRoleDto>> GetAllRole(CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllRole(cancellationToken);
        }

        public async Task<PagedList<GetAllUsersInfoDto>> GetUserSearch(
        GetSearchQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserSearch(request, cancellationToken);
        }

        public async Task<GetUserInfoDto> GetUserShowInfo(GetUserShowInfoQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserShowInfo(request, cancellationToken);
        }
    }
}
