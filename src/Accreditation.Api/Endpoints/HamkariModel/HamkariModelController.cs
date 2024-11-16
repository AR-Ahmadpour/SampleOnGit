using Accreditation.Application.HamkariModels.GetList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.HamkariModel
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class HamkariModelController : ControllerBase
    {
        private readonly ISender _sender;

        public HamkariModelController(ISender sender)
        {
            _sender = sender;
        }


        [Authorize]
        [HttpGet("Get/List/HamkariModel")]
        public async Task<IActionResult> GetListHamkariModel(CancellationToken cancellationToken)
        {
            var query = new GetListHamkariModelQuery();
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
