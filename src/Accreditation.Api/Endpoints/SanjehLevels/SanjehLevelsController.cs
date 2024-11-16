using Accreditation.Application.SanjehLevels.GetList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.SanjehLevels
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class SanjehLevelsController : ControllerBase
    {
        private readonly ISender _sender;
        public SanjehLevelsController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("sanjehlevel/list")]
        public async Task<IActionResult> GetSelectListSnjehLevel(CancellationToken cancellationToken)
        {
            var query = new GetListSanjehLevelQuery();
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
