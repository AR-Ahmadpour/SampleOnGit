using Accreditation.Application.InstanceType.Getlist;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.InstanceTypes;

[ApiController]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class InstanceTypeController : ControllerBase
{
    private readonly ISender _sender;
    public InstanceTypeController(ISender sender)
    {
        _sender = sender;
    }

    // [Authorize(Roles = "StaffChief,StaffManager,UniMember")]
    [Authorize]
    [HttpGet("instancetype/select-list")]
    public async Task<IActionResult> GetSelectListEtebarDoreh(CancellationToken cancellationToken)
    {
        var query = new InstanceTypeQuery();
        var result = await _sender.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}

