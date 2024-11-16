using Accreditation.Api.Endpoints.Users.AddRoleOrganization;
using Accreditation.Api.Endpoints.Users.AddRoleUser;
using Accreditation.Api.Endpoints.Users.AddUserPermision;
using Accreditation.Api.Endpoints.Users.EditRoleOrganization;
using Accreditation.Api.Endpoints.Users.EditRoleUser;
using Accreditation.Api.Endpoints.Users.LogIns;
using Accreditation.Api.Endpoints.Users.Registers;
using Accreditation.Application.Common.Enums;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Users.AddPermission;
using Accreditation.Application.Users.AddRoleOrganization;
using Accreditation.Application.Users.AddRoleUser;
using Accreditation.Application.Users.DeleteUserPermission;
using Accreditation.Application.Users.EditRoleOrganization;
using Accreditation.Application.Users.EditRoleUser;
using Accreditation.Application.Users.GetAllPersons;
using Accreditation.Application.Users.GetByNationalId;
using Accreditation.Application.Users.GetByParameters;
using Accreditation.Application.Users.GetCurrentUserInfo;
using Accreditation.Application.Users.GetPermisionByCategory;
using Accreditation.Application.Users.GetRoleHospital;
using Accreditation.Application.Users.GetUserPermissionByUserID;
using Accreditation.Application.Users.LogInUser;
using Accreditation.Application.Users.RegisterUser;
using Accreditation.Application.Users.Roles.GetAllRoleUser;
using Accreditation.Application.Users.Roles.GetAllRoleUserOrganization;
using Accreditation.Application.Users.Roles.GetListRoleUserUniversity;
using Accreditation.Application.Users.Roles.GetRoleByIsSetadi;
using Accreditation.Application.Users.Roles.GetRoleUserById;
using Accreditation.Application.Users.Roles.GetSelectedRole;
using Accreditation.Application.Users.ToggleUserPermission;
using Accreditation.Application.Users.UserToggleState;
using Asp.Versioning;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accreditation.Api.Endpoints.Users
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ICurrentUser _currentUser;

        public UserController(ISender sender, ICurrentUser currentUser)
        {
            _sender = sender;
            _currentUser = currentUser;
        }

        [HttpPost("user/Register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var command = new RegisterUserCommand(
                 request.Password, request.FirstName, request.LastName, request.FatherName,
                 request.BirthCertNo, request.BirthCertSerial, request.BirthPlace,
                 request.Mobile, request.Tel, request.Email, request.GenderId,
                 request.DeathDate, request.IsAlive, request.SabteAhvalApproved,
                 request.IsDeleted, request.ImageId, request.NationalCode, request.BirthDate,
                 request.AtbaKhareji, 1, request.Password);

            Result<Guid> result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }


        [HttpPost("user/login")]
        public async Task<IActionResult> login(LogInUserRequest request)
        {
            var command = new LogInUserCommand(request.NationalCode, request.Password);

            var result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }

        [Authorize]
        [HttpPost("AddRoleUser")]
        public async Task<IActionResult> AddRoleUser
         ([Required, FromBody] AddRoleUserRequest request)
        {
            var command = new AddRoleUserCommand(
                             request.RolesID,
                             request.UserGuid,
                             new Guid(_currentUser.UserId),
                             request.UniversityID
                            );

            var result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [Authorize]
        [HttpPut("EditRoleUser")]
        public async Task<IActionResult> EditRoleUser
        ([Required, FromBody] EditRoleUserRequest request)
        {
            var command = new EditRoleUserCommand(
                request.RoleUserId,
                request.RolesID,
                request.UserGuid,
                new Guid(_currentUser.UserId),
                request.UniversityID,
                request.IsActive
                );

            var result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [Authorize]
        [HttpPost("AddRoleOrganization")]
        public async Task<IActionResult> AddRoleOrganization
        ([Required, FromBody] AddRoleOrganizationRequest request)
        {
            var command = new AddRoleOrganizationCommand(
                             request.RolesID,
                             request.UserGuid,
                             new Guid(_currentUser.UserId),
                             request.OrganizationID
                            );

            var result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [Authorize]
        [HttpPut("EditRoleOrganization")]
        public async Task<IActionResult> EditRoleOrganization
       ([Required, FromBody] EditRoleOrganizationRequest request)
        {
            var command = new EditRoleOrganizationCommand(
                             request.RoleUserId,
                             request.RolesID,
                             request.UserGuid,
                             new Guid(_currentUser.UserId),
                             request.OrganizationID,
                             request.IsActive
                            );

            var result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }


        [Authorize]
        [HttpGet("GetByNationalCode/{nationalCode}")]
        public async Task<IActionResult> GetByNationalCode(string nationalCode)
        {
            var query = new GetUserByNationallCodeQuery(nationalCode);

            var result = await _sender.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [Authorize]
        [HttpGet("GetRoleByIsSetadi/{IsSetadi}")]
        public async Task<IActionResult> GetRoleByIsSetadi
        (bool IsSetadi)
        {
            var query = new GetRoleByIsSetadiQuery(IsSetadi);
            var Result = await _sender.Send(query);
            if (Result.IsSuccess)
            {
                return Ok(Result.Value);
            }
            return BadRequest(Result.Error);
        }


        [Authorize]
        [HttpGet("GetRoleInHospital")]
        public async Task<IActionResult> GetRoleInHospital()
        {
            var query = new GetRoleHospitalQuery(true);
            var Result = await _sender.Send(query);
            if (Result.IsSuccess)
            {
                return Ok(Result.Value);
            }
            return BadRequest(Result.Error);
        }


        //[Authorize]
        //[HttpGet("GetRoleUserDetail/{Id}")]
        //public async Task<IActionResult> GetRoleUserDetail
        //(int Id)
        //{
        //    var query = new GetRoleUserDetailQuery(Id);
        //    var Result = await _sender.Send(query);
        //    if (Result.IsSuccess)
        //    {
        //        return Ok(Result.Value);
        //    }
        //    return BadRequest(Result.Error);
        //}


        [Authorize]
        [HttpGet("GetAllRoleUser")]
        public async Task<IActionResult> GetAllRoleUser(
             [FromQuery] string? FullName, [FromQuery] string? NationalCode
            , [FromQuery] int? RoleID, [FromQuery] bool IsSetadi
            , [FromQuery] int pageNumber, [FromQuery] int pageSize
           )
        {
            var user = _currentUser.UserId;
            var PageList = new PagingParams();
            PageList.PageSize = pageSize;
            PageList.PageNumber = pageNumber;
            var query = new GetAllRoleUserQuery(PageList, FullName, NationalCode, RoleID, IsSetadi);


            var Result = await _sender.Send(query);
            if (Result.IsSuccess)
                return Ok(Result.Value);

            return BadRequest(Result.Error);
        }



        [Authorize]
        [HttpGet("GetAllRoleUserOrganization")]
        public async Task<IActionResult> GetAllRoleUserOrganization(
             [FromQuery] string? FullName, [FromQuery] string? NationalCode, [FromQuery] int? RoleID
            , [FromQuery] int pageNumber, [FromQuery] int pageSize
           )
        {
            var user = _currentUser.UserId;
            var PageList = new PagingParams();
            PageList.PageSize = pageSize;
            PageList.PageNumber = pageNumber;
            var query = new GetAllRoleUserOrganizationQuery(PageList, FullName, NationalCode, RoleID);


            var Result = await _sender.Send(query);
            if (Result.IsSuccess)
                return Ok(Result.Value);

            return BadRequest(Result.Error);
        }




        [Authorize]
        [HttpGet("GetRoleUserById")]
        public async Task<IActionResult> GetRoleUserById(
            [FromQuery] GetRoleUserByIdQuery Query
            )
        {
            var Result = await _sender.Send(Query);
            if (Result.IsSuccess)
                return Ok(Result.Value);

            return BadRequest(Result.Error);
        }

        [HttpGet("GetListRoleUserUniversity")]
        public async Task<IActionResult> GetListRoleUserUniversity
        ([FromQuery] GetListRoleUserUniversityQuery query)
        {
            var Result = await _sender.Send(query);
            if (Result.IsSuccess)
                return Ok(Result.Value);

            return BadRequest(Result.Error);
        }


        [HttpGet("GetPermissionByUser")]
        public async Task<IActionResult> GetPermissionByUser
        ([FromQuery] GetPermissionByUserQuery query)
        {
            var Result = await _sender.Send(query);
            if (Result.IsSuccess)
                return Ok(Result.Value);

            return BadRequest(Result.Error);
        }

        [Authorize]
        [HttpGet("SearchUser")]
        public async Task<IActionResult> SearchUser(
          [FromQuery] GetUserByParametersQuery query
            )
        {
            var Result = await _sender.Send(query);
            if (Result.IsSuccess)
                return Ok(Result.Value);

            return BadRequest(Result.Error);
        }

        [Authorize]
        [HttpGet("CurrentUserInfo")]
        public async Task<IActionResult> CurrentUserInfo
        (
           [FromQuery] GetCurrentUserInfoQuery query
        )
        {
            var Result = await _sender.Send(query);
            if (Result.IsSuccess)
                return Ok(Result.Value);

            return BadRequest(Result.Error);
        }


        [Authorize(Roles = PermisionEnum.GetAllUsers)]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] string? NationalCode,
            [FromQuery] string? Name,
            [FromQuery] int pageNumber, 
            [FromQuery] int pageSize
           )
        {
            var user = _currentUser.UserId;
            var PageList = new PagingParams();
            PageList.PageSize = pageSize;
            PageList.PageNumber = pageNumber;
            {
                var query = new GetAllUsersQuery(NationalCode, Name, PageList);

                var result = await _sender.Send(query);

                if (result.IsSuccess)
                {
                    return Ok(result.Value);
                }
                return BadRequest(result.Error);
            }
        }


        [Authorize(Roles = PermisionEnum.ToggleStateUser )]
        [HttpPatch("User/ToggleState/{Uid}")]
        public async Task<IActionResult> UserToggleState(Guid Uid, CancellationToken cancellationToken)
        {
            var query = new UserToggleStateCommand(Uid);
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result.Error);
        }



        [Authorize]
        [HttpGet("GetPermisionByCategory/select-list")]
        public async Task<IActionResult> GetPermisionByCategory(
            [FromQuery] int CategoryId, CancellationToken cancellationToken)
        {
            {
                var query = new GetPermisionByCategoryQuery(CategoryId);
                var result = await _sender.Send(query);

                if (result.IsSuccess)
                {
                    return Ok(result.Value);
                }
                return BadRequest(result.Error);
            }
        }

        [Authorize]
        [HttpPost("AddUserPermision")]
        public async Task<IActionResult> AddUserPermision
        ([Required, FromBody] AddUserPermissionRequest request)
        {
            var command = new AddUserPermissionCommand(
                             request.UserGUID,
                             request.PermissionId,
                             request.CreateByGUID,
                             request.IsAllowed
                            );

            var result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(result.Error);
        }

        
        [Authorize]
        [HttpGet("GetUserPermissionByUserId")]
        public async Task<IActionResult> GetUserPermissionByUserId(
            [FromQuery] Guid UserID, CancellationToken cancellationToken)
        {
            {
                var query = new GetUserPermissionByUserIDQuery(UserID);
                var result = await _sender.Send(query);

                if (result.IsSuccess)
                {
                    return Ok(result.Value);
                }
                return BadRequest(result.Error);
            }
        }

        [Authorize]
        [HttpDelete("UserPermission/Delete/{UserPermissionID}")]
        public async Task<IActionResult> DeleteUserPermission(
        [FromRoute] int UserPermissionID,
         CancellationToken cancellationToken)
        {
            {
                var query = new DeleteUserPermissionCommand(UserPermissionID);
                var result = await _sender.Send(query, cancellationToken);

                if (result.IsSuccess)
                {
                    return Ok(result.IsSuccess);
                }
                return BadRequest(result.Error);
            }
        }

        [Authorize]
        [HttpPatch("UserPermission/Toggle/{UserPermissionID}")]
        public async Task<IActionResult> ToggleUserPermission(
        [FromRoute] int UserPermissionID,
        CancellationToken cancellationToken)
        {
            {

                var query = new ToggleUserPermissionCommand(UserPermissionID,(new Guid( _currentUser.UserId) ));
                var result = await _sender.Send(query, cancellationToken);

                if (result.IsSuccess)
                {
                    return Ok(result.IsSuccess);
                }
                return BadRequest(result.Error);
            }
        }

    }
}
