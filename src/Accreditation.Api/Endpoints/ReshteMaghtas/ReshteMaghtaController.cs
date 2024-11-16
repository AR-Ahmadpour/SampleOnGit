using Accreditation.Application.ReshtehMaghtas.GetList;

using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.ReshteMaghtas
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ReshteMaghtaController : ControllerBase
    {
        private readonly ISender _sender;

        public ReshteMaghtaController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("Get/List/Reshteh/{maghtaGuid}")]
        public async Task<IActionResult> GetReshtehByMaghtaTahsili([FromRoute] Guid maghtaGuid,CancellationToken cancellationToken)
        {
            var query = new GetListReshtehMaghtaTahsiliQuery(maghtaGuid);
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
