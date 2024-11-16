using Accreditation.Application.DorehAmoozeshis.GetList;
using Accreditation.Application.Sanjehs.LogicalDelete;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.DorehAmoozeshis
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DorehAmoozeshiController : ControllerBase
    {
        private readonly ISender _sender;

        public DorehAmoozeshiController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("Get/List/DorehAmoozeshi")]

        public async Task<IActionResult> GetListDorehAmoozeshi(CancellationToken cancellationToken)
        {
            var query = new GetListDorehAmoozeshiQuery();
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }
    }
}
