//using Accreditation.Application.Services.UserServices.Dtos;
using Accreditation.Application.Common;
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
using Accreditation.Domain.Users;
using Accreditation.Infrastructure.Database;
using Accreditation.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using SharedKernel;





namespace Accreditation.Infrastructure.Repositories;

internal sealed class UserRepository(AccreditationDbContext context, IHashPassWord _hashPassWord) : IUserRepository
{
    //private readonly DbSet<User> _users;

    public void Add(User user)
    {
        //foreach (Role role in user.Roles)
        //{
        //    context.Attach(role);
        //}

        context.Add(user);
    }

    public void AddRoleUser(RoleUser roleUser)
    {
        context.RoleUsers.Add(roleUser);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Set<User>()
            .FirstOrDefaultAsync(user => user.GUID == id, cancellationToken);
    }

    public async Task<GetUserResponse?> GetByNationalCodeAsync(string NationalCode, CancellationToken cancellationToken = default)
    {
        return await context.Set<User>()
            .Where(user => user.NationalCode == NationalCode)
            .Select(_ => new GetUserResponse
            {
                GUID = _.GUID,
                FirstName = _.FirstName,
                LastName = _.LastName,
                FatherName = _.FatherName,
                BirthCertNo = _.BirthCertNo,
                BirthCertSerial = _.BirthCertSerial,
                BirthPlace = _.BirthPlace,
                Mobile = _.Mobile,
                Tel = _.Tel,
                Email = _.Email,
                GenderId = _.GenderId,
                DeathDate = _.DeathDate,
                IsAlive = _.IsAlive,
                SabteAhvalApproved = _.SabteAhvalApproved,
                IsDeleted = _.IsDeleted,
                ImageId = _.ImageId,
                NationalCode = _.NationalCode,
                BirthDate = _.BirthDate,
                AtbaKhareji = _.AtbaKhareji,
                SSOUserId = _.SSOUserId,
                CreatedDate = _.CreatedDate,
                UpdatedDate = _.UpdatedDate,
                CreatedByGUID = _.CreatedByGUID,
                UpdatedByGUID = _.UpdatedByGUID,
                Password = _.Password,
                MobileConfirmed = _.MobileConfirmed
            }
            )
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Role>> GetRoleByUserAsync(Guid userid, CancellationToken cancellationToken = default)
    {
        try
        {
            return await
                        context.Set<RoleUser>()
                        // .Include(u => u.RoleUsers) .Include(u => u.Roles)
                        .Where(ru => ru.UsersGUID == userid)
                        .Select(_ => new Role
                        {
                            Id = _.RolesId,
                            Title = _.Role.Title,
                            Description = _.Role.Description,
                        })
                        .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<GetRoleByIsSetadiDto>> GetAllRoleAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<Role>()
            .Select(_ => new GetRoleByIsSetadiDto
            {
                Id = _.Id,
                Title = _.Title,
                Description = _.Description,
                IsDeleted = _.IsDeleted,
                IsSetadi = _.IsSetadi,
            })
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// خروجی براساس وضعیت فیلد ستادی و حذف منطقی
    /// </summary>
    /// <param name="IsSetadi"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<GetRoleByIsSetadiDto>> GetRoleByIsSetadiAsync(bool IsSetadi, CancellationToken cancellationToken = default)
    {

        return await context.Set<Role>()
            .Where(rol => rol.IsSetadi == IsSetadi && !rol.IsDeleted)
            .Select(_ => new GetRoleByIsSetadiDto
            {
                Id = _.Id,
                Title = _.Title,
                Description = _.Description,
                IsDeleted = _.IsDeleted,
                IsSetadi = _.IsSetadi,
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<GetRoleUserDetailDto?> GetRoleDetailAsync(GetRoleUserDetailQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            return await context.Users.Where(ur => ur.GUID == request.UserId && ur.RoleUsers.Any(ru => ru.RolesId == request.RoleId))
                                        .SelectMany(u => u.RoleUsers, (u, ru) => new GetRoleUserDetailDto
                                        {
                                            NationalCode = u.NationalCode,
                                            FullName = u.FirstName + " " + u.LastName, // Assuming User has FirstName and LastName properties
                                            Id = ru.Role.Id,
                                            Title = ru.Role.Title,
                                            Description = ru.Role.Description,
                                            IsDeleted = ru.Role.IsDeleted,
                                            IsSetadi = ru.Role.IsSetadi
                                        })
                                        .FirstOrDefaultAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<PagedList<GetAllRoleUserDto>> GetAllRoleUserAsync(GetAllRoleUserQuery request, CancellationToken cancellationToken = default)
    {
        //var roleUserDtoQuery = context.Users
        //    .Include(u => u.RoleUsers)
        //    .ThenInclude(ru => ru.Role)
        //    .Where(u => u.RoleUsers.Any(ru => ru.Role.IsSetadi == request.IsSetadi))
        //    .SelectMany(u => u.RoleUsers.Where(ru => ru.Role.IsSetadi == request.IsSetadi), (u, ru) => new GetAllRoleUserDto
        //    {
        //        NationalCode = ru.User.NationalCode,
        //        FullName = ru.User.FirstName + " " + ru.User.LastName,
        //        UserId = ru.User.GUID,
        //        RoleId = ru.Role.Id,
        //        RoleUserId=ru.Id,
        //        Title = ru.Role.Title,
        //        Description = ru.Role.Description,
        //        UniversityTitel=ru.UniversityMember!.University.Title,
        //        UniversityMemberId=ru.UniversityMember!.Id,
        //        IsActive = ru.IsActive
        //    })
        //    .AsQueryable();


        var roleUserDtoQuery = context.Users
    .Include(u => u.RoleUsers)
        .ThenInclude(ru => ru.Role)
    .Include(u => u.RoleUsers)
        .ThenInclude(ru => ru.UniversityMember)  // Include UniversityMember
            .ThenInclude(um => um.University)    // Include University
    .Where(u => u.RoleUsers.Any(ru => ru.Role.IsSetadi == request.IsSetadi))
    .SelectMany(u => u.RoleUsers.Where(ru => ru.Role.IsSetadi == request.IsSetadi), (u, ru) => new GetAllRoleUserDto
    {
        NationalCode = ru.User.NationalCode,
        FullName = ru.User.FirstName + " " + ru.User.LastName,
        UserId = ru.User.GUID,
        RoleId = ru.Role.Id,
        RoleUserId = ru.Id,
        Title = ru.Role.Title,
        Description = ru.Role.Description,
        UniversityTitel = ru.UniversityMember != null ? ru.UniversityMember.University.Title : null,
        UniversityId = ru.UniversityMember != null ? ru.UniversityMember.University.Id : null,
        UniversityMemberId = ru.UniversityMember != null ? ru.UniversityMember.Id : (int?)null,
        IsActive = ru.IsActive
    })
    .AsQueryable();

        // Apply filters based on request parameters
        if (!string.IsNullOrEmpty(request.FullName))
        {
            roleUserDtoQuery = roleUserDtoQuery.Where(dto => dto.FullName.Contains(request.FullName));
        }
        if (!string.IsNullOrEmpty(request.NationalCode))
        {
            roleUserDtoQuery = roleUserDtoQuery.Where(dto => dto.NationalCode == request.NationalCode);
        }
        if (request.RoleID.HasValue)
        {
            roleUserDtoQuery = roleUserDtoQuery.Where(dto => dto.RoleId == request.RoleID);
        }
        roleUserDtoQuery = roleUserDtoQuery.Where(res => res.UniversityMemberId != null);

        return await PagedList<GetAllRoleUserDto>.Paginate(
               source: roleUserDtoQuery,
               pageNumber: request.PagingParams.PageNumber,
               pageSize: request.PagingParams.PageSize,
               cancellationToken: cancellationToken);


        //return await paginator.Paginate(RoleUserDto, request.criteria.PageNumber, request.criteria.PageSize, cancellationToken);

    }

    public async Task<PagedList<GetAllRoleUserOrganizationDto>> GetAllRoleUserOrgAsync(GetAllRoleUserOrganizationQuery request, CancellationToken cancellationToken = default)
    {

        var roleUserDtoQuery = context.Users
        .Include(u => u.RoleUsers)
        .ThenInclude(ru => ru.Role)
        .Include(u => u.RoleUsers)
        .ThenInclude(ru => ru.organizationMember)  // Include OrganizationMember
            .ThenInclude(um => um.Organization)    // Include Organization
        //.Where(u => u.RoleUsers.Any(ru => ru.Role.IsSetadi == request.IsSetadi))
        .SelectMany(u => u.RoleUsers, (u, ru) => new GetAllRoleUserOrganizationDto
        {
            NationalCode = ru.User.NationalCode,
            FullName = ru.User.FirstName + " " + ru.User.LastName,
            UserId = ru.User.GUID,
            RoleId = ru.Role.Id,
            RoleUserId = ru.Id,
            Title = ru.Role.Title,
            Description = ru.Role.Description,
            OrganizationTitel = ru.organizationMember != null ? ru.organizationMember.Organization.Name : null,
            OrganizationGuid = ru.organizationMember != null ? ru.organizationMember.Organization.GUID : null,
            OrganizationMemberId = ru.organizationMember != null ? ru.organizationMember.Id : (int?)null,
            IsActive = ru.IsActive
        })
        .AsQueryable();

        // Apply filters based on request parameters
        if (!string.IsNullOrEmpty(request.FullName))
        {
            roleUserDtoQuery = roleUserDtoQuery.Where(dto => dto.FullName.Contains(request.FullName));
        }
        if (!string.IsNullOrEmpty(request.NationalCode))
        {
            roleUserDtoQuery = roleUserDtoQuery.Where(dto => dto.NationalCode == request.NationalCode);
        }
        if (request.RoleID.HasValue)
        {
            roleUserDtoQuery = roleUserDtoQuery.Where(dto => dto.RoleId == request.RoleID);
        }

        roleUserDtoQuery = roleUserDtoQuery.Where(res => res.OrganizationGuid != null);


        return await PagedList<GetAllRoleUserOrganizationDto>.Paginate(
               source: roleUserDtoQuery,
               pageNumber: request.PagingParams.PageNumber,
               pageSize: request.PagingParams.PageSize,
               cancellationToken: cancellationToken);
    }


    public async Task<PagedList<GetRoleUserByIdDto>> GetRoleUserByIdAsync(GetRoleUserByIdQuery query, CancellationToken cancellationToken = default)
    {
        var RoleUserDto = context.Set<User>()
      .Include(u => u.RoleUsers)
          .ThenInclude(ru => ru.Role)
      .SelectMany(u => u.RoleUsers.Where(ru => ru.UsersGUID == query.UserID && ru.IsActive), (u, ru) => new GetRoleUserByIdDto
      {
          NationalCode = ru.User.NationalCode,
          FullName = ru.User.FirstName + " " + ru.User.LastName,
          UserId = ru.User.GUID,
          RoleId = ru.Role.Id,
          Title = ru.Role.Title,
          Description = ru.Role.Description,
          UniversityTitel = "نام دانشگاه",
          UniversityId = 0
      })
        .AsQueryable();

        return await PagedList<GetRoleUserByIdDto>.Paginate(
               source: RoleUserDto,
               pageNumber: query.PagingParams.PageNumber,
               pageSize: query.PagingParams.PageSize,
               cancellationToken: cancellationToken);
    }


    public async Task<List<Permission>?> GetPermissionAsync(string NationalCode, CancellationToken cancellationToken = default)
    {
        List<Permission> Permissions = new List<Permission>();
        //TODO 1403-03-21
        return Permissions;
    }

    public async Task<LogInUserDto?> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            string hashPassWord = _hashPassWord.Encryption(password);
            return await context.Set<User>()
               .Where(user => user.NationalCode == username && user.Password == hashPassWord)
                .Select(_ => new LogInUserDto
                {
                    UserGuid = _.GUID,
                    FullName = _.FirstName + ' ' + _.LastName,
                    NationalCode = _.NationalCode,
                    TempToken = DateTime.Now.ToString()
                })
                .FirstOrDefaultAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            throw;
        }
    }

    public async Task<User?> GetUserById(Guid username, CancellationToken cancellationToken = default)
    {
        try
        {
            return await context.Set<User>()
                .FirstOrDefaultAsync(user => user.GUID == username, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            throw;
        }
    }

    /// <summary>
    /// لیست نقش افراد که شامل دانشگاه و سازمان میباشد
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PagedList<GetListRoleUserUniversityDto>> GetListRoleUserUniversityAsync(GetListRoleUserUniversityQuery query, CancellationToken cancellationToken = default)
    {
        #region Old Code Call By EF
        //var query1 = from ru in context.Set<RoleUser>()
        //                 join r in context.Set<Role>() on ru.RolesId equals r.Id
        //                 join um in context.Set <UniversityMember>() on ru.Id equals um.RoleUserId
        //                 where ru.UsersGUID == query.UserId && ru.IsActive &&
        //                       new[] { "StaffChief", "StaffManager", "StaffAgent", "ArzyabKeshvari" }.Contains(r.Title)
        //                 select new GetListRoleUserUniversityDto
        //                 {
        //                     RoleId = r.Id,
        //                     RoleTitle = r.Title,
        //                     UniversityId= um.UniversityId,
        //                     RoleDescription=r.Description,
        //                     OrganizationGUID= (Guid?)null
        //                 };

        //    var query2 = from ru in context.Set<RoleUser>()
        //                 join r in context.Set<Role>() on ru.RolesId equals r.Id
        //                 join um in context.Set<UniversityMember>() on ru.Id equals um.RoleUserId
        //                 join u in context.Universities on um.UniversityId equals u.Id
        //                 where ru.UsersGUID == query.UserId && ru.IsActive &&
        //                       new[] { "UniMember", "UniMember" }.Contains(r.Title)
        //                 select new GetListRoleUserUniversityDto
        //                 {
        //                     RoleId = r.Id,
        //                     RoleTitle = r.Title,
        //                     UniversityId = um.UniversityId,
        //                     RoleDescription = r.Description + " " + u.Title,
        //                     OrganizationGUID = (Guid?)null
        //                 };

        //    var query3 = (from ru in context.Set<RoleUser>()
        //                  join r in context.Set<Role>() on ru.RolesId equals r.Id
        //                  join um in context.Set<UniversityMember>() on ru.Id equals um.RoleUserId
        //                  where ru.UsersGUID == query.UserId && ru.IsActive &&
        //                        r.Title == "ArzyabDaneshgahi"
        //                  select new GetListRoleUserUniversityDto
        //                  {
        //                      RoleId = r.Id,
        //                      RoleTitle = r.Title,
        //                      UniversityId=um.UniversityId,
        //                      RoleDescription= r.Description,
        //                      OrganizationGUID = (Guid?)null
        //                  }).Distinct();

        //    var query4 = from ru in context.Set<RoleUser>()
        //                 join r in context.Set<Role>() on ru.RolesId equals r.Id
        //                join om in context.Set<OrganizationMember>() on ru.Id equals om.RoleUserId
        //                join o in context.Set<Organization>() on om.OrganizationGUID equals o.GUID
        //                 join ot in context.Set<OrgType>() on o.GUID equals ot.GUID
        //                 where ru.UsersGUID == query.UserId && ru.IsActive
        //                 select new GetListRoleUserUniversityDto
        //                 {
        //                     RoleId = r.Id,
        //                     RoleTitle = r.Title,
        //                     UniversityId = o.UniversityId,
        //                     RoleDescription = r.Description + " " + ot.Title + " " + o.Name,
        //                     OrganizationGUID=om.OrganizationGUID
        //                 };

        //    var result = query1.Union(query2).Union(query3).Union(query4).OrderBy(x => x.RoleId).AsQueryable();
        #endregion
        #region Second Test
        // var userIdParam = new SqlParameter("@UserId",query.UserId);

        // var result = await context.Set<GetListRoleUserUniversityDto>()
        //     .FromSqlRaw("EXEC GetListRoleUserUniversity @UserId", userIdParam)
        //     .ToListAsync(cancellationToken);

        // return await PagedList<GetListRoleUserUniversityDto>.Paginate(
        //source: result.AsQueryable(),
        //pageNumber: 1,
        //pageSize: 10,
        //cancellationToken: cancellationToken);
        #endregion


        var result = await context.GetListRoleUserUniversityAsync(query.UserId, cancellationToken);

        return await PagedList<GetListRoleUserUniversityDto>.Paginate(
            source: result.AsQueryable(),
            pageNumber: 1,
            pageSize: 30,
            cancellationToken: cancellationToken);

    }

    public async Task<List<GetPermissionByUserDto>> GetPermissionByUser(GetPermissionByUserQuery query, CancellationToken cancellationToken = default)
    {
        #region Old
        //var rolePermissions = await context.Set<Permission>()
        //    .Include(p => p.RolePermissions)
        //    .ThenInclude(rp => rp.Role)
        //    .Where(p => p.RolePermissions.Any(rp => rp.RoleId == query.RoleId))
        //    .SelectMany(p => p.RolePermissions.Where(rp => rp.RoleId == query.RoleId), (permission, rolePermission) => new GetPermissionByUserDto
        //    {
        //        RoleId = rolePermission.Role.Id,
        //        RoleTitle = rolePermission.Role.Title,
        //        PermissionId = permission.Id,
        //        PermissionIsDeleted = permission.IsDeleted,
        //        PermissionTitle = permission.Title,
        //        Allowed = true,
        //        Desvrp = permission.Desvrp,
        //    })
        //    .ToListAsync(cancellationToken);

        //// Query permissions from UserPermission
        //var userPermissions = await context.Set<Permission>()
        //    .Include(p => p.UserPermissions)
        //    .ThenInclude(up => up.User)
        //    .Where(p => p.UserPermissions.Any(up => up.UserGUID == query.UserId))
        //    .SelectMany(p => p.UserPermissions.Where(up => up.UserGUID == query.UserId), (permission, userPermission) => new GetPermissionByUserDto
        //    {

        //        PermissionId = permission.Id,
        //        PermissionIsDeleted = permission.IsDeleted,
        //        PermissionTitle = permission.Title,
        //        Allowed = true,
        //        Desvrp = permission.Desvrp,
        //    })
        //    .ToListAsync(cancellationToken);

        //// Combine the results
        //var result = rolePermissions.Concat(userPermissions).ToList();
        #endregion

        try
        {
            // Query permissions from RolePermission
            var rolePermissions = await context.RolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .Where(rp => rp.RoleId == query.RoleId)
                .Select(rp => new GetPermissionByUserDto
                {
                    RoleId = rp.Role.Id,
                    RoleTitle = rp.Role.Title,
                    PermissionId = rp.Permission.Id,
                    PermissionIsDeleted = rp.Permission.IsDeleted,
                    PermissionTitle = rp.Permission.Title,
                    Allowed = true,
                    Desvrp = rp.Permission.Description,
                })
                .ToListAsync(cancellationToken);

            var userPermissions = await context.UserPermissions
                .Include(up => up.User)
                .Include(up => up.Permission)
                .Where(up => up.UserGUID == query.UserId)
                .Select(up => new GetPermissionByUserDto
                {
                    //UserId = up.UserGUID,
                    PermissionId = up.Permission.Id,
                    PermissionIsDeleted = up.Permission.IsDeleted,
                    PermissionTitle = up.Permission.Title,
                    Allowed = up.IsAllowed,
                    Desvrp = up.Permission.Description,
                })
                .ToListAsync(cancellationToken);

            var result = rolePermissions.Concat(userPermissions).ToList();
            var distinctPermissions = result.Distinct(new GetPermissionByUserDtoComparer()).ToList();


            return distinctPermissions;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<string?> GetCurrentRoleName(int RoleId, Guid UserId, CancellationToken cancellationToken = default)
    {
        try
        {
            var role = await
                     context.Set<RoleUser>()
                     .Where(ru => ru.RolesId == RoleId && ru.UsersGUID == UserId)
                     .Select(_ => new Role
                     {
                         Id = _.RolesId,
                         Title = _.Role.Title,
                         Description = _.Role.Description,
                     })
                     .FirstOrDefaultAsync(cancellationToken);

            //var role=await context.Roles.Where(r => r.Id == RoleId).FirstOrDefaultAsync(cancellationToken);
            return role.Title;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<Result<List<GetUserByParametersDto>>> GetUserByParameters(GetUserByParametersQuery query, CancellationToken cancellationToken)
    {
        #region Old
        //var usersQuery = context.Users.AsQueryable();

        //if (!string.IsNullOrEmpty(query.FirstName))
        //{
        //    usersQuery = usersQuery.Where(u => u.FirstName.Contains(query.FirstName));
        //}

        //if (!string.IsNullOrEmpty(query.LastName))
        //{
        //    usersQuery = usersQuery.Where(u => u.LastName.Contains(query.LastName));
        //}

        //if (!string.IsNullOrEmpty(query.NationalCode))
        //{
        //    usersQuery = usersQuery.Where(u => u.NationalCode.Contains(query.NationalCode));
        //}

        //return await usersQuery
        //                .Select(user => new GetUserByParametersDto
        //                {
        //                    UserID = user.GUID,
        //                    FirstName = user.FirstName,
        //                    LastName = user.LastName,
        //                    NationalCode = user.NationalCode,
        //                    Mobile = user.Mobile,
        //                    Email = user.Email,
        //                    IsAlive = user.IsAlive,
        //                    IsDeleted = user.IsDeleted
        //                })
        //                .ToListAsync(cancellationToken);

        #endregion

        return await context.Users
                .Where(u =>
                          (u.FirstName + " " + u.LastName).Contains(query.SearchParam)
                          || u.NationalCode.Contains(query.SearchParam)
                        )
                .Select(user => new GetUserByParametersDto
                {
                    UserID = user.GUID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    NationalCode = user.NationalCode,
                    Mobile = user.Mobile,
                    Email = user.Email,
                    IsAlive = user.IsAlive,
                    IsDeleted = user.IsDeleted
                })
                .ToListAsync(cancellationToken);
    }

    public async Task<RoleUser?> RoleUserFindAsync(int RoleUserId, CancellationToken cancellationToken = default)
    {
        return await context.RoleUsers.FindAsync(RoleUserId, cancellationToken);
    }

    public async Task<GetCurrentUserInfoQueryDto?> GetCurrentUserInfo(GetCurrentUserInfoQuery query, CancellationToken cancellationToken = default)
    {
        return await context.Users
       .Include(user => user.UserInfo)
       //.ThenInclude(ui => ui.)
       .Where(use => use.GUID == query.UserId)
       .Select(CurrentUser => new GetCurrentUserInfoQueryDto
       {

           //Userid=
           //Username = CurrentUser.,
           FirstName = CurrentUser!.FirstName,
           LastName = CurrentUser!.LastName,
           FatherName = CurrentUser!.FatherName,
           NationalCode = CurrentUser!.NationalCode,
           BirthDate = CurrentUser!.BirthDate.ToString(),
           Email = CurrentUser!.Email,
           PhoneNumber = "",
           Ostan = CurrentUser!.UserInfo.AddressOstanId.ToString(),
           City = CurrentUser!.UserInfo.AddressOstanId.ToString(),
           Address = CurrentUser!.UserInfo.Address,
           OrganizationType = "",
           Organization = "",
           Univercity = "",




       })
       .FirstOrDefaultAsync(cancellationToken);



        //var result = rolePermissions.Concat(userPermissions).ToList();
        //var distinctPermissions = result.Distinct(new GetPermissionByUserDtoComparer()).ToList();

    }

    public async Task<PagedList<GetAllUsersDto>> GetAllUsers(GetAllUsersQuery query, CancellationToken cancellationToken = default)
    {
        var GetAllUsers = context.Users
                .Where(u => u.NationalCode != null)
                .Select(user => new GetAllUsersDto
                {

                    UserGuid = user.GUID,
                    UserName = user.NationalCode,
                    FullName = user.FirstName + " " + user.LastName,
                    IsDelete = user.IsDeleted
                }).AsQueryable();

        // Apply filters based on request parameters
        if (!string.IsNullOrEmpty(query.Name))
        {
            GetAllUsers = GetAllUsers.Where(dto => dto.FullName.Contains(query.Name));
        }
        if (!string.IsNullOrEmpty(query.NationalCode))
        {
            GetAllUsers = GetAllUsers.Where(dto => dto.UserName == query.NationalCode);
        }

        return await PagedList<GetAllUsersDto>.Paginate(
               source: GetAllUsers,
               pageNumber: query.PagingParams.PageNumber,
               pageSize: query.PagingParams.PageSize,
               cancellationToken: cancellationToken);
    }

    public async Task<List<GetPermisionByCategoryDto>> GetPermisionByCategory(GetPermisionByCategoryQuery query, CancellationToken cancellationToken)
    {
        var GetPermisionByCategory = await context.Permissions
            .Where(per => per.IsDeleted == false && per.CategoryId == query.CategoryID)
            .Select(per => new GetPermisionByCategoryDto
            {
                Id = per.Id,
                Title = per.Title
            })
            .AsNoTracking()
            .ToListAsync();
        GetPermisionByCategoryDto temp = new GetPermisionByCategoryDto();
        temp.Id = -1;
        temp.Title = "انتخاب کنید";
        GetPermisionByCategory.Insert(0, temp);
        return GetPermisionByCategory;
    }

    public void AddUserPermission(AddUserPermissionCommand command, CancellationToken cancellationToken)
    {
        UserPermission userPermission = new UserPermission
        {
            UserGUID = command.UserGUID,
            CreateByGUID = command.CreateByGUID,
            IsAllowed = command.IsAllowed,
            PermissionId = command.PermissionId,
            CreateDate = DateTime.Now
        };

        context.UserPermissions.Add(userPermission);
    }

    public async Task<bool> UserPermissionFindAync(Guid UserId, int PermissionId)
    {
        return context.UserPermissions
            .Where(up => up.UserGUID == UserId && up.PermissionId == PermissionId).Any();
    }
    public async Task<UserPermission?> UserPermissionEsixt( int UserPermissionId)
    {
        return await context.UserPermissions
            .Where(up =>   up.Id == UserPermissionId).FirstOrDefaultAsync();
    }
   
    public void DeleteUserPermission(UserPermission userPermission)
    {
        context.UserPermissions.Remove(userPermission);
    }

    public async Task<List<GetUserPermissionByUserIdDto>> GetUserPermissionByUserId(GetUserPermissionByUserIDQuery query, CancellationToken cancellationToken)
    {




        //var GetUserPermissionByUserid =await (from userp in context.UserPermissions
        //             join per in context.Permissions on userp.PermissionId equals per.Id
        //             join CreateBy in context.Users on userp.CreateByGUID equals CreateBy.GUID
        //             join cat in context.Categories on per.CategoryId equals cat.Id
        //             join UpdateBy in context.Users on userp.UpdateByGUID equals UpdateBy.GUID
        //             where userp.UserGUID == query.UserID                            
        //             select new GetUserPermissionByUserIdDto
        //             {
        //                 UserPermissionID = userp.PermissionId,
        //                 PermissionTitel = per.Title,
        //                 CategoryTitel = cat.Title,
        //                 CreateUserFullName = CreateBy != null ? CreateBy.FirstName + " " + CreateBy.LastName : "",
        //                 UpdateFullName = UpdateBy != null ? UpdateBy.FirstName + " " + UpdateBy.LastName : "",
        //                 Allow = userp.IsAllowed == true ? "دارد" : "ندارد"
        //             }).ToListAsync();


        var GetUserPermissionByUserid = await (from userp in context.UserPermissions
                                               join per in context.Permissions on userp.PermissionId equals per.Id
                                               join CreateBy in context.Users on userp.CreateByGUID equals CreateBy.GUID
                                               join cat in context.Categories on per.CategoryId equals cat.Id
                                               join UpdateBy in context.Users on userp.UpdateByGUID equals UpdateBy.GUID into updateByJoin
                                               from UpdateBy in updateByJoin.DefaultIfEmpty() // This handles the left join for nullable UpdateByGUID
                                               where userp.UserGUID == query.UserID
                                               select new GetUserPermissionByUserIdDto
                                               {
                                                   UserPermissionID = userp.Id,
                                                   PermissionTitel = per.Title,
                                                   CategoryTitel = cat.Title,
                                                   CreateUserFullName = CreateBy != null ? CreateBy.FirstName + " " + CreateBy.LastName + " " + PersianDateHelper.ToPersianDateTimeString(userp.CreateDate) : "",
                                                   UpdateFullName = UpdateBy != null
                                                       ? UpdateBy.FirstName + " " + UpdateBy.LastName + " " + (userp.UpdateDate.HasValue ? PersianDateHelper.ToPersianDateTimeString(userp.UpdateDate.Value) : "")
                                                       : "",
                                                   Allow = userp.IsAllowed //userp.IsAllowed == true ? "دارد" : "ندارد"
                                               }).ToListAsync();
        return GetUserPermissionByUserid;

    }




}

