using Accreditation.Api.Endpoints.ZirMehvars.Adds;
using Accreditation.Api.Endpoints.ZirMehvars.Edits;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Mehvars.GetListMehvarsInArzyabiDakheli;
using Accreditation.Application.ZirMehvars.Add;
using Accreditation.Application.ZirMehvars.Edit;
using Accreditation.Application.ZirMehvars.GetById;
using Accreditation.Application.ZirMehvars.GetList;
using Accreditation.Application.ZirMehvars.GetListZirMehvarsInArzyabiDakheli;
using Accreditation.Application.ZirMehvars.LogicalDelete;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Accreditation.Api.Endpoints.ZirMehvars
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class ZirMehvarsController : ControllerBase
    {
        private readonly ISender _sender;
        public ZirMehvarsController(ISender sender)
        {
            _sender = sender;
        }


        [Authorize]
        [HttpPost("zirmehvar")]
        public async Task<IActionResult> AddZirMehvar([FromBody] AddZirMehvarRequest request,
                                                       CancellationToken cancellationToken)
        {
            var command = new AddZirMehvarCommand(request.MehvarGuid, request.Title, 
                                                  request.SortOrder, request.WeightedCoefficient);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }

        [Authorize]
        [HttpPut("zirmehvar/edit/{id}")]
        public async Task<IActionResult> EditEtebareDore(Guid id,
                                                      [FromBody] EditZirMehvarRequest request,
                                                      CancellationToken cancellationToken)
        {
            var command = new EditZirMehvarCommand(id, request.title,
                                                   request.weightedCoefficient,
                                                  request.sortOrder);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }

        [Authorize]
        [HttpGet("zirmehvar/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetZirMehvarByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result.Value);
        }


        [Authorize]
        [HttpGet("zirmehvar/list/{mehvarId}")]
        public async Task<IActionResult> GetEtebarDorehList(Guid mehvarId,
                                                           [FromQuery] PagingParams criteria,
                                                            CancellationToken cancellationToken)
        {
            var query = new GetListZirMehvarQuery(mehvarId, criteria.PageNumber, criteria.PageSize);
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("zirmehvar/logical-delete/{id}")]
        public async Task<IActionResult> DeleteEtebrDoreh(Guid id, CancellationToken cancellationToken)
        {
            var query = new LogicalDeleteZirMehvarCommand(id);
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result.Error);
        }

    [Authorize]
    [HttpGet("Zirmehvar/GetListZirMehvarsInArzyabiDakheli")]
     public async Task<IActionResult> GetListZirMehvarsInArzyabiDakheli(
    [Required, FromQuery] Guid MehvarId,
    [Required, FromQuery] bool SanjehNA,
    [Required, FromQuery] bool SanjehMe,
    [Required, FromQuery] int SanjehLevelId,
    [Required, FromQuery] Guid AccInstanceID,
    [Required, FromQuery] Guid EtebardorehId,
    CancellationToken cancellationToken)

        {
            var query = new GetListZirMehvarsInArzyabiDakheliQuery(MehvarId, SanjehNA, SanjehMe, SanjehLevelId, AccInstanceID, EtebardorehId);
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

    }
}
