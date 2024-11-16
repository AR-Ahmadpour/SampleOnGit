using Accreditation.Application.AccreditationalInstanceAnswers.Edit;
using Accreditation.Application.AccreditationalInstanceAnswers.GetListAnswerMehvar;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Accreditation.Api.Endpoints.AccreditationalInstanceAnswers;

[ApiController]
[Route("api/v{version:apiVersion}")]
[ApiVersion("1.0")]
public class AccreditationalInstanceAnswerController : ControllerBase
{
    private readonly ISender _sender;
    public AccreditationalInstanceAnswerController(ISender sender)
    {
        _sender = sender;
    }


    [Authorize]
    [HttpGet("accreditationalInstanceanswer/list")]
    public async Task<IActionResult> GetListAccreditationInstance([FromQuery] Guid accreditationInstanceGUID,
                                                                  CancellationToken cancellationToken)
    {
        //TODO
        var query = new GetListAccreditationalInstanceAnswerMehvarQuery(accreditationInstanceGUID );
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [Authorize]
    [HttpPut("AccreditationalInstanceanswer/SaveResultAccInsAnswer")]
    public async Task<IActionResult> SaveResultAccInsAnswer
        ([FromBody] EditAccreditationalInstanceAnswerCommand command,
         CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);
        if(result.IsSuccess)
            return Ok(result.Value);
        return BadRequest(result.Error);
    }


}
