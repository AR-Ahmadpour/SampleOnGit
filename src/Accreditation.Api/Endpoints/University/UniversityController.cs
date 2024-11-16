using Accreditation.Application.Universityes.GetSelectList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.University;

[ApiController]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class UniversityController : ControllerBase
{
    private readonly ISender _sender;
    public UniversityController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet("university/select-list")]
    public async Task<IActionResult> GetSelectListEtebarDoreh(CancellationToken cancellationToken)
    {
        var query = new GetUniversitySelectListQuery();
        var result = await _sender.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
