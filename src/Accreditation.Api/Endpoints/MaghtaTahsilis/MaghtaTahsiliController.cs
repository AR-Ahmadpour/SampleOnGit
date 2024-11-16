using Accreditation.Application.MaghtaTahsilis.GetList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.MaghtaTahsilis
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class MaghtaTahsiliController : ControllerBase
    {
        private readonly ISender _sender;

        public MaghtaTahsiliController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("Get/List/MaghtaTahsili")]

        public async Task<IActionResult> GetListMaghtaTahsili(CancellationToken cancellationToken)
        {
            var query = new GetListMaghtaTahsiliQuery();
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
