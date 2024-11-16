using Accreditation.Api.Endpoints.AccreditationInstances.Edits;
using Accreditation.Application.AccreditationInstances.Add;
using Accreditation.Application.AccreditationInstances.Delete;
using Accreditation.Application.AccreditationInstances.Edit;
using Accreditation.Application.AccreditationInstances.GetByEtebarDorehGUID;
using Accreditation.Application.AccreditationInstances.GetById;
using Accreditation.Application.AccreditationInstances.GetList;
using Accreditation.Application.AccreditationInstances.GetListBasedMasters;
using Accreditation.Application.AccreditationInstances.GetSelectLists;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.AccreditationInstance;

[ApiController]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class AccreditationInstanceController : ControllerBase
{
    private readonly ISender _sender;
    public AccreditationInstanceController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpPost("accreditationalInstance")]
    public async Task<IActionResult> CreateAccinstance([FromBody] AddAccreditationalInstanceCommand request,
                                                  CancellationToken cancellationToken)
    {
        var command = new AddAccreditationalInstanceCommand(request.EtebarDorehGUID,
                                                       request.OrganizationGuid,
                                                       request.InstanceTypeId,
                                                       request.FromDate,
                                                       request.ToDate,
                                                       request.ArzyabSarparastGuid,
                                                       request.AccreditationInstanceGuid);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Error);
    }

    [Authorize]
    [HttpPut("accreditationalInstance/edit/{id}")]
    public async Task<IActionResult> UpdateAccreditationmojadad([FromRoute] Guid id,
                                                                [FromBody] EditMojadadRequest request,
                                                                CancellationToken cancellationToken)
    {
        var command = new EditAccInstanceCommand(id, request.fromDate,
                                                             request.toDate,
                                                             request.ArzyabSarparastGuid);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Error);
    }

    [Authorize]
    [HttpDelete("accreditationalInstance/delete/{id}")]
    public async Task<IActionResult> DeleteAccreditationInstance([FromRoute] Guid id,
                                                               CancellationToken cancellationToken)
    {
        var command = new DeleteAccInstanceCommand(id);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.IsSuccess);
        else
            return BadRequest(result.Error);
    }

    [Authorize]
    [HttpGet("accreditationalInstance/getbyid/{id}")]
    public async Task<IActionResult> GetAccreditationInstanceById(Guid id,
                                                                  CancellationToken cancellationToken)
    {
        var query = new GetAccreditationalInstanceByIdQuery(id);
        var result = await _sender.Send(query, cancellationToken);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [Authorize]
    [HttpGet("accreditationalInstance/getbased-masterguid/{id}")]
    public async Task<IActionResult> GetAccreditationInstanceByMasterGuid(Guid id,
                                                                          CancellationToken cancellationToken)
    {
        var query = new GetListBasedMasterQuery(id);
        var result = await _sender.Send(query, cancellationToken);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [Authorize]
    [HttpGet("accreditationalInstance/list")]
    public async Task<IActionResult> GetListAccreditationInstance([FromQuery]
                                                                 int instanceTypeId,
                                                                 Guid etebarDorehGUID,
                                                                 Guid organizationGuid,
                                                                 CancellationToken cancellationToken)
    {
        var query = new GetListAccreditationalInstanceQuery(instanceTypeId,
                                                            etebarDorehGUID,
                                                            organizationGuid);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [Authorize]
    [HttpGet("accreditationalInstancepayeh/select-list")]
    public async Task<IActionResult> GetSelectListPayehAccreditationInstance([FromQuery]
                                                                        int instanceTypeId,
                                                                        Guid etebarDorehGUID,
                                                                        Guid organizationGuid,
                                                                        CancellationToken cancellationToken)
    {
        var query = new GetListPayehAccreditationalInstanceQuery(instanceTypeId,
                                                            etebarDorehGUID,
                                                            organizationGuid);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }
    [Authorize]
    [HttpGet("GetAccreditationInstanceByEtebarDorehId")]
    public async Task<IActionResult> GetAccreditationInstanceByEtebarDorehId(Guid EtebarDorehId,
                                                                 CancellationToken cancellationToken)
    {
        var query = new GetAccreditationInstanceByEtebarDorehIdQuery(EtebarDorehId);
        var result = await _sender.Send(query, cancellationToken);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }
}