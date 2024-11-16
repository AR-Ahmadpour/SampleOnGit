using Accreditation.Application.Universityes.GetAll;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.Universites
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class UniversityController : Controller
    {
        private readonly ISender _sender;

        public UniversityController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("GetAllUniversity")]
        public async Task<IActionResult> GetAllUniversity()
        {
            var query = new GetAllUniversityQuery(true);

            var result = await _sender.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

    }
}
