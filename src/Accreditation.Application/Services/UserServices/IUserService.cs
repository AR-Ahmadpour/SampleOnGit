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
    public interface IUserService
    {
        Task<string> AddRoleToUser(AddRoleToUserCommand request, CancellationToken cancellationToken);

        Task<PagedList<GetAllUsersInfoDto>> GetAllUsersInfo(GetAllUsersInfoQuery request, CancellationToken cancellationToken);

        Task<GetUserInfoDto> GetUserInfo(GetUserInfoQuery request, CancellationToken cancellationToken);

        Task<List<GetAllRoleDto>> GetAllRole(CancellationToken cancellationToken);

        Task<PagedList<GetAllUsersInfoDto>> GetUserSearch(GetSearchQuery request, CancellationToken cancellationToken);

        Task<GetUserInfoDto> GetUserShowInfo(GetUserShowInfoQuery request, CancellationToken cancellationToken);
    }
}
