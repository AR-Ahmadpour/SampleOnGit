using Accreditation.Api.Endpoints.Standards.Adds;
using Accreditation.Api.Endpoints.Standards.Edits;
using Accreditation.Application.Common.Models;
using Accreditation.Application.Standards.Add;
using Accreditation.Application.Standards.Edit;
using Accreditation.Application.Standards.GetById;
using Accreditation.Application.Standards.GetList;
using Accreditation.Application.Standards.GetListStandardsInArzyabiDakheli;
using Accreditation.Application.Standards.LogicalDelete;
using Accreditation.Application.ZirMehvars.GetListZirMehvarsInArzyabiDakheli;
using Accreditation.Domain.Mehvars.Entities;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace Accreditation.Api.Endpoints.Standards
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class StandardsController : ControllerBase
    {
        private readonly ISender _sender;
        public StandardsController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost("standard")]
        public async Task<IActionResult> CreateStandards([FromBody] AddStandardRequest request,
                                                 CancellationToken cancellationToken)
        {
            var command = new AddStandardCommand(request.ZirMehvarGuid, request.Title,
                                                 request.ShortTitle, request.Code,
                                                 request.WeightedCoefficient, request.SortOrder);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }

        [Authorize]
        [HttpGet("standard/list/{zirmehvarId}")]
        public async Task<IActionResult> ListStandards([FromQuery] PagingParams criteria,
                                                     Guid zirmehvarId,
                                                     CancellationToken cancellationToken)
        {
            var query = new GetListStandardQuery(zirmehvarId, criteria.PageNumber, criteria.PageSize);
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [Authorize]
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> FindStandardById(Guid id,
                                                        CancellationToken cancellationToken)
        {
            var query = new GetStandardByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [HttpPatch("standard/logical-delete/{id}")]
        public async Task<IActionResult> DeleteStandards(Guid id,
                                                        CancellationToken cancellationToken)
        {
            var query = new LogicalDeleteStandardCommand(id);
            Result result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result.Error);
        }

        [Authorize]
        [HttpPut("standard/{id}")]
        public async Task<IActionResult> EditStandard(Guid id,
        [FromBody] EditStandardRequest request,
        CancellationToken cancellationToken)
        {
            var command = new EditStandardCommand(
                id, request.Title, request.ShortTitle, request.Code,
                request.WeightedCoefficient, request.SortOrder);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }

        [Authorize]
        [HttpGet("standard/GetListStandardsInArzyabiDakheli")]
        public async Task<IActionResult> GetListStandardsInArzyabiDakheli
        ([Required, FromQuery] Guid ZirMehvarGUID,
        [Required ,FromQuery] bool SanjehNA,
        [Required, FromQuery] bool SanjehMe,
        [Required, FromQuery] int SanjehLevelId,
        [Required, FromQuery] Guid AccInstanceID,
        [Required, FromQuery] Guid EtebardorehId,
        CancellationToken cancellationToken)
        {
            var query = new GetListStandardsInArzyabiDakheliQuery(ZirMehvarGUID, SanjehNA, SanjehMe, SanjehLevelId, AccInstanceID,EtebardorehId);
            var result = await _sender.Send(query, cancellationToken);
            if(result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
    }
}
