using Accreditation.Api.Endpoints.SanjehEnvironmentStandards.Adds;
using Accreditation.Application.EnvironmentStandards.GetSelectList;
using Accreditation.Application.SanjehEnvironmentStandards.Add;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading;
using Accreditation.Application.SanjehEnvironmentStandards.GetList;

namespace Accreditation.Api.Endpoints.SanjehEnvironmentStandards
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class SanjehEnvironmentStandardsController : ControllerBase
    {
        private readonly ISender _sender;
        public SanjehEnvironmentStandardsController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("sanjeh-environment-standards/list")]
        public async Task<IActionResult> GetSelectListSnjehEnvironment(CancellationToken cancellationToken)
        {
            var query = new GetEnvironmentStandardSelectListQuery();
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [HttpPost("Add-Sanjeh-environment-Standard")]

        public async Task<IActionResult> AddSanjehEnvironmentStandard([FromBody] AddSanjehEnvironmentStandardRequest request,CancellationToken cancellationToken)
        {
            var command = new AddSanjehEnvironmentStandardCommand(request.SanjehGuid,request.EnvironmentStandardsGuids);
            var result = await _sender.Send(command, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetBy/{SanjehId}")]

        public async Task<IActionResult> GetListEnvironmentStandardBySanjehId([FromRoute]Guid SanjehId,CancellationToken cancellationToken)
        {
            var query = new GetListSanjehEnvironmentStandardBySanjehIdQuery(SanjehId);
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
