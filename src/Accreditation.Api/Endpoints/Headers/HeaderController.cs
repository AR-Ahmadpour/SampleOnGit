using Accreditation.Application.Headers.GetBy;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.Headers
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class HeaderController : ControllerBase
    {
        private readonly ISender _sender;

        public HeaderController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("Get/Excel/Header")]

        public async Task<IActionResult> GetHeader([FromQuery] Guid FieldGuid, [FromQuery] Guid AccreditationInstanceGuid,
            CancellationToken cancellationToken)
        {
            var query = new GetHeaderQuery(FieldGuid,AccreditationInstanceGuid);

            var result = await _sender.Send(query,cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }
    }
}
