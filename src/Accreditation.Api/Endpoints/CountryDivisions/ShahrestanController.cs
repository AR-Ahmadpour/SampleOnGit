using Accreditation.Application.CountryDivisions.Shahrestan.GetSelectList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.CountryDivisions
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class ShahrestanController : ControllerBase
    {
        private readonly ISender _sender;
        public ShahrestanController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("shahrestan/select-list/{ostanId}")]
        public async Task<IActionResult> GetSelectListEtebarDoreh(int ostanId, CancellationToken cancellationToken)
        {
            var query = new GetShahrestanSelectListQuery(ostanId);
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
