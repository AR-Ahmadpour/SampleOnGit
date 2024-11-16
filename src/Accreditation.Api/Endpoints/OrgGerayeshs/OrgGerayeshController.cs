using Accreditation.Application.OrgGerayesh.GetSelectList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.OrgGerayesh;

[ApiController]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class OrgGerayeshController : ControllerBase
{
    private readonly ISender _sender;
    public OrgGerayeshController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet("orggerayesh/select-list")]
    public async Task<IActionResult> GetSelectListEtebarDoreh([FromQuery] Guid orgTypeGuid, CancellationToken cancellationToken)
    {
        var query = new GetOrgGerayeshSelectListQuery(orgTypeGuid);
        var result = await _sender.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
