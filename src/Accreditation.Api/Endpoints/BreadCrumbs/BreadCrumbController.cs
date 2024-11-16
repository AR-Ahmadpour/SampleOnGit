using Accreditation.Application.BreadCrumbs.Get;
using Accreditation.Application.Sanjehs.GetList;
using Accreditation.Domain.Standards.Entities;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.BreadCrumbs
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BreadCrumbController : ControllerBase
    {
        private readonly ISender _sender;

        public BreadCrumbController(ISender sender)
        {
            _sender = sender;
        }


        [Authorize]
        [HttpGet("BreadCrumb")]
        public async Task<IActionResult> BreadCrumb([FromQuery] Guid? GUID, [FromQuery] string? GuidType,
            CancellationToken cancellationToken)
        {
            var query = new GetBreadCrumbQuery(GUID,GuidType);
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
