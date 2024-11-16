using Accreditation.Api.Endpoints.Mehvars.Adds;
using Accreditation.Api.Endpoints.Mehvars.Edits;
using Accreditation.Application.Common.Models;
using Accreditation.Application.EtebarDorehs.LogicalDelete;
using Accreditation.Application.Mehvars.Add;
using Accreditation.Application.Mehvars.Edit;
using Accreditation.Application.Mehvars.GetById;
using Accreditation.Application.Mehvars.GetList;
using Accreditation.Application.Mehvars.GetListMehvarsInArzyabiDakheli;
using Accreditation.Domain.SanjehLevels.Entities;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace Accreditation.Api.Endpoints.Mehvars
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class MehvarsController : ControllerBase
    {
        private readonly ISender _sender;
        public MehvarsController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost("mehvar")]
        public async Task<IActionResult> CreateMehvars([FromBody] AddMehvarRequest request,
                                                 CancellationToken cancellationToken)
        {
            var command = new AddMehvarCommand(request.EtebarDorehGuid, request.Title,
                                               request.SortOrder, request.WeightedCoefficient);
            var result = await _sender.Send(command, cancellationToken);
          
            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }

        [Authorize]
        [HttpGet("mehvar/list/{EtebardorehId}")]
        public async Task<IActionResult> ListMehvars([FromQuery] PagingParams criteria,
                                                     Guid EtebardorehId,
                                                     CancellationToken cancellationToken)
        {
            var query = new GetListMehvarQuery(EtebardorehId, criteria.PageNumber, criteria.PageSize);
            var result = await _sender.Send(query, cancellationToken);
            
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [Authorize]
        [HttpGet("mehvar/List/EtebarDoreh/{EtebardorehId}")]

        public async Task<IActionResult> ListOfMehvars(Guid EtebardorehId,CancellationToken cancellationToken)
        {
            var query = new GetListOfMehvarQuery(EtebardorehId);
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [Authorize]
        [HttpGet("mehvar/{id}")]
        public async Task<IActionResult> FindMehvarById(Guid id,
                                                        CancellationToken cancellationToken)
        {
            var query = new GetMehvarByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result.Value);
        }

        [Authorize]
        [HttpPatch("mehvar/logical-delete/{id}")]
        public async Task<IActionResult> SOFTDeleteMehvar(Guid id,
                                                        CancellationToken cancellationToken)
        {
            var query = new LogicalDeleteMehvarCommand(id);
            Result result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result.Error);
        }

        [Authorize]
        [HttpPut("mehvar/{id}")]
        public async Task<IActionResult> EditMehvar(Guid id,
                                                       [FromBody] EditMehvarRequest request,
                                                       CancellationToken cancellationToken)
        {
            var command = new EditMehvarCommand(id, request.Title,
                                                request.WeightedCoefficient,
                                                request.SortOrder);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }

        [Authorize]
        [HttpGet("mehvar/GetListMehvarsInArzyabiDakheli")]
        public async Task<IActionResult> GetListMehvarsInArzyabiDakheli(
            [Required,FromQuery] Guid EtebardorehId,
            [Required,FromQuery] bool SanjehNA,
            [Required,FromQuery] bool SanjehMe,
            [Required, FromQuery] int SanjehLevelId,
            [Required, FromQuery] Guid AccInstanceID, 
            CancellationToken cancellationToken)
        {
            var query = new GetListMehvarsInArzyabiDakheliQuery(EtebardorehId, SanjehNA,SanjehMe, SanjehLevelId, AccInstanceID);
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }


    }
}
