using Accreditation.Application.NoeKhedmats.GetList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Accreditation.Api.Endpoints.NoeKhedmat
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class NoeKhedmatController : ControllerBase
    {
        private readonly ISender _sender;

        public NoeKhedmatController(ISender sender)
        {
            _sender = sender;
        }


        [Authorize]
        [HttpGet("Get/List/NoeKhedmat")]

        public async Task<IActionResult> GetListNoeKhedmat(CancellationToken cancellationToken)
        {
            var query = new GetListNoeKhedmatQuery();
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
    }
}
