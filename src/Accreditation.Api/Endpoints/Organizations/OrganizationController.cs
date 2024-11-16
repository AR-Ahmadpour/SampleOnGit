using Accreditation.Application.Common.Models;
using Accreditation.Application.Organization.GetList;
using Accreditation.Application.Organizations.GetById;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.Organization;

[ApiController]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class OrganizationController : ControllerBase
{
    private readonly ISender _sender;
    public OrganizationController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet("organization/select-list")]
    public async Task<IActionResult> GetSelectListEtebarDoreh([FromQuery] PagingParams criteria,
                                                    Guid? OrgTypeGuid,
                                                    Guid? OrgGerayeshGuid,
                                                    int? OstanId,
                                                    int? ShahrestanId,
                                                    int? BakhshLocationId,
                                                    int? ShahrId,
                                                    int? UnivaersityId,
                                                    string? OrganizationName,
                                                   CancellationToken cancellationToken)
    {
        var query = new GetListOrganizationQuery(criteria.PageNumber,
                                                 criteria.PageSize,
                                                 OrgTypeGuid,
                                                 OrgGerayeshGuid,
                                                 OstanId,
                                                 ShahrestanId,
                                                 BakhshLocationId,
                                                 ShahrId,
                                                 UnivaersityId,
                                                 OrganizationName);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [Authorize]
    [HttpGet("organizationByGuid/{id}")]
    public async Task<IActionResult> FindOrganizationById(Guid id,
                                                   CancellationToken cancellationToken)
    {
        var query = new GetOrganizationByIdQuery(id);
        var result = await _sender.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result.Value);
    }

}
