using Accreditation.Api.Endpoints.UserInfos.Adds;
using Accreditation.Api.Endpoints.UserInfos.Edits;
using Accreditation.Application.UserInfos.Add;
using Accreditation.Application.UserInfos.Edit;
using Accreditation.Application.UserInfos.GetById;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.UserInfos
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserInfoController : ControllerBase
    {
        private readonly ISender _sender;

        public UserInfoController(ISender sender)
        {
            _sender = sender;
        }



        [Authorize]
        [HttpPost("Add/UserInfo")]
        public async Task<IActionResult> AddUserInfo([FromBody] AddUserInfoRequest request, CancellationToken cancellationToken)
        {
            var command = new AddUserInfoCommand(
                request.UserGuid,
                request.Address,
                request.MaritalStatus,
                request.ChildCount,
                request.BirthOstandId,
                request.BirthShahrId,
                request.AddressOstanId,
                request.AddressShahrId,
                request.PersonalPicGUID,
                request.KartMeliGUID,
                request.ShenasnamehGUID);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }


        [Authorize]
        [HttpPut("Edit/UserInfo/{UserInfoId}")]

        public async Task<IActionResult> EditUserInfo([FromRoute] Guid UserInfoId, [FromBody] EditUserInfoRequest request,
            CancellationToken cancellationToken)
        {
            var command = new EditUserInfoCommand(
            UserInfoId,
            request.Address,
            request.MaritalStatus,
            request.ChildCount,
            request.BirthOstanId,
            request.BirthShahrId,
            request.AddressOstanId,
            request.AddressShahrId,
            request.PersonalPicGUID,
            request.KartMeliGUID,
            request.ShenasnamehGUID
            );

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }


        [Authorize]
        [HttpGet("Get/UserInfo/By/{UserGuid}")]

        public async Task<IActionResult> GetUserInfoByUserGuid([FromRoute] Guid UserGuid, CancellationToken cancellationToken)
        {
            var query = new GetUserInfoByUserGuidQuery(UserGuid);
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

    }
}
