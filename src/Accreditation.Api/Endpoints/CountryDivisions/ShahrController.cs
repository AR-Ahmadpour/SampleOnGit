using Accreditation.Application.CountryDivisions.Shahr.GetSelectList;
using Accreditation.Application.CountryDivisions.Shahrestan.GetSelectList;
using Accreditation.Application.Sanjehs.GetList;
using Accreditation.Domain.Standards.Entities;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.CountryDivisions
{

    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class ShahrController : ControllerBase
    {
        private readonly ISender _sender;
        public ShahrController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpGet("shahr/select-list/{bakhshId}")]
        public async Task<IActionResult> GetSelectListShahr(int bakhshId, CancellationToken cancellationToken)
        {
            var query = new GetShahrByBakhshSelectListQuery(bakhshId);
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)

                return NotFound();

            return Ok(result);
        }


        [Authorize]
        [HttpGet("shahr/Get-List/{ostanId}")]

        public async Task<IActionResult> GetSelectListShahrByOstanId(int ostanId, CancellationToken cancellationToken)
        {
            var query = new GetShahrSelectListQuery(ostanId);
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
