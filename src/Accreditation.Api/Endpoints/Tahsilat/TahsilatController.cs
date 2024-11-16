using Accreditation.Api.Endpoints.Tahsilat.Adds;
using Accreditation.Api.Endpoints.Tahsilat.Edits;
using Accreditation.Application.Tahsilats.Add;
using Accreditation.Application.Tahsilats.Edit;
using Accreditation.Application.Tahsilats.GetById;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.Tahsilat
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TahsilatController : ControllerBase
    {
        private readonly ISender _sender;

        public TahsilatController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost("Add/Tahsilat")]
        public async Task<IActionResult> AddTahsilat([FromBody] AddTahsilatRequest request, CancellationToken cancellationToken)
        {
            var command = new AddTahsilatCommand(
                request.UserGuid,
                request.ReshtehTahsiliGuid,
                request.MaghtaTahsiliGuid,
                request.MadrakGuid,
                request.UniversityName,
                request.GraduationDate);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }


        [Authorize]
        [HttpPut("Edit/Tahsilat/{TahsilatGuid}")]

        public async Task<IActionResult> EditTahsilat([FromBody] EditTahsilatRequest request, [FromRoute] Guid TahsilatGuid, CancellationToken cancellationToken)
        {
            var command = new EditTahsilatCommand(
               TahsilatGuid,
               request.ReshtehTahsiliGuid,
               request.MaghtaTahsiliGuid,
               request.MadrakGuid,
               request.UniversityName,
               request.GraduationDate);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }


        [Authorize]
        [HttpGet("Get/Tahsilat/{UserGuid}")]

        public async Task<IActionResult> GetTahsilatByUserGuid([FromRoute] Guid UserGuid, CancellationToken cancellationToken)
        {
            var query = new GetTahsilatByUserGuidQuery(UserGuid);
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
