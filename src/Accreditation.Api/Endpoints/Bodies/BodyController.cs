using Accreditation.Application.Body.GetByAccreditationInstanceGuid;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.Bodies
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BodyController : ControllerBase
    {
        private readonly ISender _sender;

        public BodyController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("Get/Body")]

        public async Task<IActionResult> GetBody([FromQuery] Guid AccreditationInstanceGuid, Guid FieldGuid,CancellationToken cancellationToken)
        {
            var query = new GetByAccreditationInstanceGuidQuery(AccreditationInstanceGuid,FieldGuid);

            var result = await _sender.Send(query,cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }



        [Authorize]
        [HttpGet("Get/Body/Mommayezi")]

        public async Task<IActionResult> GetBodyMomayezi([FromQuery] Guid AccreditationInstanceGuid, Guid FieldGuid, CancellationToken cancellationToken)
        {
            var query = new GetBodyByAccreditationInstanceGuidQueryMommayezi(FieldGuid, AccreditationInstanceGuid);

            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }
    }
}
