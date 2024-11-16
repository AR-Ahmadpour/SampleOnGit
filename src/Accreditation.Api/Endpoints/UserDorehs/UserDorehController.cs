using Accreditation.Api.Endpoints.UserDorehs.Adds;
using Accreditation.Api.Endpoints.UserDorehs.Edits;
using Accreditation.Application.UserDorehs.Add;
using Accreditation.Application.UserDorehs.Edit;
using Accreditation.Application.UserDorehs.GetById;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.UserDorehs
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserDorehController : ControllerBase
    {
        private readonly ISender _sender;

        public UserDorehController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost("Add/UserDoreh")]
        public async Task<IActionResult> AddUserDoreh([FromBody] AddUserDorehRequest request, CancellationToken cancellationToken)
        {
            var command = new AddUserDorehCommand(
                request.UserGuid,
                request.DorehAmoozeshiGuid,
                request.DorehTitle,
                request.BargozarKonnandeh,
                request.DorehHours,
                request.DorehRole);

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
        [HttpPut("Edit/UserDoreh/{UserDorehGuid}")]
        public async Task<IActionResult> EditUserDoreh([FromRoute] Guid UserDorehGuid,[FromBody] EditUserDorehRequest request,CancellationToken cancellationToken)
        {
            var command = new EditUserDorehCommand(
                UserDorehGuid,
                request.DorehAmoozeshiGUID,
                request.DorehTitle,
                request.BargozarKonandeh,
                request.DorehHours,
                request.DorehRole
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


        [HttpGet("Get/UserDoreh/By/{UserGuid}")]

        public async Task<IActionResult> GetUserDorehByUserGuid([FromRoute] Guid UserGuid, CancellationToken cancellationToken)
        {
            var query = new GetUserDorehByUserGuidQuery(UserGuid);
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
