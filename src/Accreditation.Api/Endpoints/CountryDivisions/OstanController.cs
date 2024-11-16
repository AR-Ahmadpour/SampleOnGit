using Accreditation.Application.CountryDivisions.Ostan.GetSelectList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.CountryDivisions;

[ApiController]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class OstanController : ControllerBase
{
    private readonly ISender _sender;
    public OstanController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet("ostan/select-list")]
    public async Task<IActionResult> GetSelectListEtebarDoreh(CancellationToken cancellationToken)
    {
        var query = new GetOstanSelectListQuery();
        var result = await _sender.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
