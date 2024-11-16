using Accreditation.Application.EnvironmentStandards.GetSelectList;
using Accreditation.Application.OrgTypes.GetSelectList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.OrgTypes
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class OrgTypesController : ControllerBase
    {
        private readonly ISender _sender;
        public OrgTypesController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("org-type/select-list")]
        public async Task<IActionResult> GetSelectListOrgTypes(CancellationToken cancellationToken)
        {
            var query = new GetOrgTypeSelectListQuery();
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
