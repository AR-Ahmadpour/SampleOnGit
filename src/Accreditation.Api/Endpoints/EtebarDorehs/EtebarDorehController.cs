using Accreditation.Api.Endpoints.EtebarDorehs.Adds;
using Accreditation.Api.Endpoints.EtebarDorehs.Edits;
using Accreditation.Application.Common.Models;
using Accreditation.Application.EtebarDorehs.Add;
using Accreditation.Application.EtebarDorehs.Edit;
using Accreditation.Application.EtebarDorehs.GetById;
using Accreditation.Application.EtebarDorehs.GetList;
using Accreditation.Application.EtebarDorehs.LogicalDelete;
using Accreditation.Application.OrgTypes.GetSelectList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Accreditation.Api.Endpoints.EtebarDorehs
{

    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class EtebarDorehController : ControllerBase
    {
        private readonly ISender _sender;
        public EtebarDorehController( ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost("etebar-doreh")]
        public async Task<IActionResult> AddEtebarDoreh([FromBody] AddEtebarDorehRequest request,
                                                        CancellationToken cancellationToken)
        {
            var command = new AddEtebarDorehCommand(request.OrgTypeGUID, request.Title, 
                                                    request.StartDate, request.EndDate,
                                                    request.IsCurrent, request.SortOrder);
            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }

        [Authorize]
        [HttpPut("etebar-doreh/{id}")]
        public async Task<IActionResult> EditEtebareDore(Guid id,
                                                         [FromBody]EditEtebarDorehRequest request,
                                                         CancellationToken cancellationToken)
        {
            var command = new EditEtebarDorehCommand
            (id, request.OrgTypeGUID, request.Title, request.StartDate,
            request.EndDate, request.IsCurrent, request.SortOrder);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }

        [Authorize]
        [HttpGet("EtebarDoreh/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetEtebarDorehByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("etebar-doreh/select-list/{OrgTypeId}")]
        public async Task<IActionResult> GetSelectListEtebarDoreh(Guid OrgTypeId,
                                                                  CancellationToken cancellationToken)
        {
            var query = new GetEtebarDoreSelectListQuery(OrgTypeId);
            var result = await _sender.Send(query, cancellationToken);

             if (result == null)
                return NotFound();

            return Ok(result);
        }



        [Authorize]
        [HttpGet("etebar-doreh/list")]
        public async Task<IActionResult> GetEtebarDorehList([FromQuery] PagingParams criteria,
                                                            [FromQuery] Guid? Orgtype,
                                                            CancellationToken cancellationToken)
        {
            var query = new GetListEtebarDorehQuery(criteria.PageNumber, criteria.PageSize, Orgtype);
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("etebar-doreh/Logical-Delete/{id}")]
        public async Task<IActionResult> DeleteEtebrDoreh(Guid id, CancellationToken cancellationToken)
        {
            var query = new LogicalDeleteEtebarDorehCommand(id);
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result.Error);
        }
      
    }

}
