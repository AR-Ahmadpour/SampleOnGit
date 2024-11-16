using Accreditation.Application.Common.Models;
using Accreditation.Application.Users.AddPermission;
using Accreditation.Application.Users.GetAllPersons;
using Accreditation.Application.Users.GetById;
using Accreditation.Application.Users.GetByParameters;
using Accreditation.Application.Users.GetCurrentUserInfo;
using Accreditation.Application.Users.GetPermisionByCategory;
using Accreditation.Application.Users.GetUserPermissionByUserID;
using Accreditation.Application.Users.LogInUser;
using Accreditation.Application.Users.Roles.GetAllRoleUser;
using Accreditation.Application.Users.Roles.GetAllRoleUserOrganization;
using Accreditation.Application.Users.Roles.GetListRoleUserUniversity;
using Accreditation.Application.Users.Roles.GetPermissionByUser;
using Accreditation.Application.Users.Roles.GetRoleByIsSetadi;
using Accreditation.Application.Users.Roles.GetRoleUserById;
using Accreditation.Application.Users.Roles.GetRoleUserDetail;
using Accreditation.Application.Users.Roles.GetSelectedRole;
using SharedKernel;

namespace Accreditation.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<GetUserResponse?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken = default);
    Task<List<Role>> GetRoleByUserAsync(Guid GuidUser, CancellationToken cancellationToken = default);
    Task<List<GetRoleByIsSetadiDto>> GetAllRoleAsync(CancellationToken cancellationToken = default);
    Task<List<GetRoleByIsSetadiDto>> GetRoleByIsSetadiAsync(bool IsSetadi, CancellationToken cancellationToken = default);
    Task<GetRoleUserDetailDto> GetRoleDetailAsync(GetRoleUserDetailQuery request, CancellationToken cancellationToken = default);    
    Task<List<Permission>?> GetPermissionAsync(string NationalCode, CancellationToken cancellationToken = default);
    Task <LogInUserDto?> LoginAsync(string username,string password, CancellationToken cancellationToken = default);
    Task <PagedList<GetAllRoleUserDto>> GetAllRoleUserAsync(GetAllRoleUserQuery query, CancellationToken cancellationToken = default);  
    Task<PagedList<GetAllRoleUserOrganizationDto>> GetAllRoleUserOrgAsync(GetAllRoleUserOrganizationQuery query, CancellationToken cancellationToken = default);
    Task<PagedList<GetRoleUserByIdDto>> GetRoleUserByIdAsync(GetRoleUserByIdQuery query, CancellationToken cancellationToken = default);
    Task <User?> GetUserById(Guid username, CancellationToken cancellationToken = default);
    /// <summary>
    /// لیست نقش افراد که شامل دانشگاه و سازمان میباشد
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PagedList<GetListRoleUserUniversityDto>> GetListRoleUserUniversityAsync(GetListRoleUserUniversityQuery query, CancellationToken cancellationToken = default);
    Task <List<GetPermissionByUserDto>> GetPermissionByUser(GetPermissionByUserQuery query, CancellationToken cancellationToken = default);
    void Add(User user);
    void AddRoleUser(RoleUser roleUser);
    Task<string> GetCurrentRoleName(int RoleId, Guid UserId, CancellationToken cancellationToken = default);
    Task<Result<List<GetUserByParametersDto>>> GetUserByParameters(GetUserByParametersQuery query, CancellationToken cancellationToken);
    Task<RoleUser?> RoleUserFindAsync(int RoleUserId, CancellationToken cancellationToken = default);
    Task<GetCurrentUserInfoQueryDto> GetCurrentUserInfo(GetCurrentUserInfoQuery query ,  CancellationToken cancellationToken = default);
    Task<PagedList<GetAllUsersDto>> GetAllUsers(GetAllUsersQuery query, CancellationToken cancellationToken = default);
    Task<List<GetPermisionByCategoryDto>> GetPermisionByCategory(GetPermisionByCategoryQuery query, CancellationToken cancellationToken = default);
    void AddUserPermission(AddUserPermissionCommand command, CancellationToken cancellationToken = default);
    Task<bool> UserPermissionFindAync(Guid UserId, int PermissionId);
    Task<UserPermission?> UserPermissionEsixt(int UserPermissionId);
    void DeleteUserPermission(UserPermission userPermission);
    Task<List<GetUserPermissionByUserIdDto>> GetUserPermissionByUserId(GetUserPermissionByUserIDQuery query, CancellationToken cancellationToken);
}
