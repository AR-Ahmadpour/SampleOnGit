using Accreditation.Api.Endpoints.SanjehFields.Add;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.SanjehFields.Add;
using Accreditation.Application.SanjehFields.GetById;
using Accreditation.Application.Sanjehs.GetList;
using Accreditation.Domain.Standards.Entities;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.SanjehFields
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class SanjehFieldController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ICurrentUser _currentUser;
        public SanjehFieldController(ISender sender, ICurrentUser currentUser)
        {
            _sender = sender;
            _currentUser = currentUser;
        }

        [Authorize]
        [HttpPost("AddSanjehField")]
        public async Task<IActionResult> AddSanjehField([FromBody] AddSanjehFieldRequest request)
        {
            var command = new AddSanjehFieldCommand(request.FieldGuid,request.SanjehGuids);

            var result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }


        [Authorize]
        [HttpGet("Get/By/{fieldId}")]
        public async Task<IActionResult> GetSanjehsByFieldId([FromRoute] Guid fieldId, [FromQuery] Guid? mehvarguid
            , CancellationToken cancellationToken)
        {
            var query = new GetSanjehByFieldQuery(fieldId,mehvarguid);
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
