using Accreditation.Api.Endpoints.Fields.Adds;
using Accreditation.Api.Endpoints.Fields.Edits;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Fields.Add;
using Accreditation.Application.Fields.Edit;
using Accreditation.Application.Fields.GetById;
using Accreditation.Application.Fields.GetFilterdList;
using Accreditation.Application.Fields.GetHierarchy;
using Accreditation.Application.Fields.GetList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.Fields;
[ApiController]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class FieldController : ControllerBase
{
    private readonly ISender _sender;
    public FieldController(ISender sender)
    {
        _sender = sender;
    }



    [Authorize]
    [HttpPost("Add-Field")]

    public async Task<IActionResult> AddField([FromBody] AddFieldRequest request, CancellationToken cancellationToken)
    {
        var command = new AddFieldCommand(request.EtebarDorehGuid, request.Title, request.TitleCode, request.InstanceTypeIds);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return BadRequest(result.Error);
        }
    }


    [Authorize]
    [HttpPut("Edit-Field/{fieldid}")]

    public async Task<IActionResult> EditField(Guid fieldid, [FromBody] EditFieldRequest request, CancellationToken cancellationToken)
    {

        var command = new EditFieldCommand(fieldid, request.Title, request.TitleCode, request.InstanceTypeIds);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else
        {
            return BadRequest(result.Error);
        }
    }


    [Authorize]
    [HttpGet("Get/Hierarchy/{mehvarId}")]

    public async Task<IActionResult> GetByMehvarId([FromRoute] Guid mehvarId, CancellationToken cancellationToken)
    {
        var query = new GetListHierArchyQuery(mehvarId);
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

    [Authorize]
    [HttpGet("Get/List-Field/By/{etebarDorehId}")]

    public async Task<IActionResult> GetByEtebarDorehId([FromRoute] Guid etebarDorehId, [FromQuery] PagingParams criteria,
        CancellationToken cancellationToken)
    {
        var query = new GetlistByEtebarDorehIdQuery(etebarDorehId, criteria.PageNumber, criteria.PageSize);
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




    [Authorize]
    [HttpGet("fieldsaccreditation")]
    public async Task<IActionResult> GetById([FromQuery] Guid etebardorehGuid,
                                                         Guid accreditationalInstaneGuid,
                                                         CancellationToken cancellationToken)
    {
        var query = new GetAllFilterdFieldQuery(etebardorehGuid, accreditationalInstaneGuid);
        var result = await _sender.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result.Value);
    }
    [Authorize]
    [HttpGet("fields")]
    public async Task<IActionResult> GetAll([FromQuery] Guid etebardorehGuid,
                                                        Guid accreditationalInstaneGuid,
                                                        CancellationToken cancellationToken)
    {
        var query = new GetAllFieldQuery(etebardorehGuid, accreditationalInstaneGuid);
        var result = await _sender.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result.Value);
    }

}
