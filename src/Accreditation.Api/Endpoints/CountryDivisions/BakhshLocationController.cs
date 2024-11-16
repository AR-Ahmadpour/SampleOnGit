using Accreditation.Application.CountryDivisions.BakhshLocation.GetSelectList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.CountryDivisions
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class BakhshController : ControllerBase
    {
        private readonly ISender _sender;
        public BakhshController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("bakshlocation/select-list/{shahrestanId}")]
        public async Task<IActionResult> GetSelectListEtebarDoreh(int shahrestanId, CancellationToken cancellationToken)
        {
            var query = new GetBakhshLocationSelectListQuery(shahrestanId);
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
