using Accreditation.Api.Endpoints.NotNaSanjeh.Adds;
using Accreditation.Application.NotNaSanjehs.Add;
using Accreditation.Application.NotNaSanjehs.GetList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.NotNaSanjeh
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class NotNaSanjehController : ControllerBase
    {
        private readonly ISender _sender;

        public NotNaSanjehController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost("Add-NotNa-Sanjeh")]
        public async Task<IActionResult> AddNotNaSanjeh([FromBody] AddNotNaSanjehRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AddNotNaSanjehCommand(request.SanjehGuid, request.OrgGerayeshGuids);
            var result = await _sender.Send(command, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("Get/OrgGerayeshes/{SanjehId}")]
        public async Task<IActionResult> GetListOrgGerayeshBySanjehId([FromRoute] Guid SanjehId, CancellationToken cancellationToken)
        {
            var query = new GetListOrgGerayeshBySanjehIdQuery(SanjehId);
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
