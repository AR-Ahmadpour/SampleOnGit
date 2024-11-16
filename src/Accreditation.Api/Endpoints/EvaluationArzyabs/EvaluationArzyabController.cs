using Accreditation.Api.Endpoints.EvaluationArzyabs.Adds;
using Accreditation.Api.Endpoints.EvaluationArzyabs.Edits;
using Accreditation.Application.EvaluationArzyabs.Add;
using Accreditation.Application.EvaluationArzyabs.Delete;
using Accreditation.Application.EvaluationArzyabs.Edit;
using Accreditation.Application.EvaluationArzyabs.GetList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Accreditation.Api.Endpoints.EvaluationArzyabs;

[ApiController]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class EvaluationArzyabController : ControllerBase
{
    private readonly ISender _sender;
    public EvaluationArzyabController(ISender sender)
    {
        _sender = sender;
    }


    [Authorize]
    [HttpPost("evaluationarzyab")]
    public async Task<IActionResult> AddevaluationArzyab([FromBody] AddevaluationArzyabRequest request,
                                                                    CancellationToken cancellationToken)
    {
        var command = new AddArzyabComomand(request.ArzyabRoleId,
                                            request.FieldIds, request.EtebarDorehGuid,
                                            request.AccreditationInstanceGuid, request.ArzyabUserGuid);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Error);
    }

    [Authorize]
    [HttpPut("evaluationarzyab/edit/{id}")]
    public async Task<IActionResult> EditArzyabEvaluation(Guid id,
                                                  [FromBody] EditevaluationArzyabRequest request,
                                                  CancellationToken cancellationToken)
    {
        var command = new EditArzyabComomand(request.ArzyabRoleId,
                                            request.FieldIds,
                                            id);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);
        else
            return BadRequest(result.Error);
    }

    [Authorize]
    [HttpGet("evaluationarzyab")]
    public async Task<IActionResult> GetById([FromQuery] Guid etebardorehGuid,
                                                         Guid accreditationalInstaneGuid,
                                                         CancellationToken cancellationToken)
    {
        var query = new GetAllEvaluationArzyabQuery(etebardorehGuid, accreditationalInstaneGuid);
        var result = await _sender.Send(query, cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete("evaluationarzyab/{id}")]
    public async Task<IActionResult> DeleteById(Guid id,
                                                CancellationToken cancellationToken)
    {
        var query = new DeleteEvaluationArzyabCommand(id);
        Result result = await _sender.Send(query, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.IsSuccess);
        else
            return BadRequest(result.Error);
    }

}
