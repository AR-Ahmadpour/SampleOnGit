using Accreditation.Application.EnvironmentStandards.GetList;
using Accreditation.Application.EnvironmentStandards.GetSelectList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.EnvironmentStandards
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class EnvironmentStandardsController : ControllerBase
    {
        private readonly ISender _sender;
        public EnvironmentStandardsController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("environment-standard/select-list")]
        public async Task<IActionResult> GetSelectListEnvironmentStandard(CancellationToken cancellationToken)
        {
            var query = new GetEnvironmentStandardSelectListQuery();
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [Authorize]
        [HttpGet("environment-standard/{etebardorehid}")]

        public async Task<IActionResult> GetListEnvironmentStandardByEtebardorehId(Guid etebardorehid, CancellationToken cancellationToken)
        {
            var query = new GetListEnvironmentStandardQuery(etebardorehid);
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
