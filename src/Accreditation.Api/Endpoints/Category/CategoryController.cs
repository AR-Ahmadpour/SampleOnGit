using Accreditation.Application.Category.CategorySelectedList;
using Accreditation.Application.OrgGerayesh.GetSelectList;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.Category
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoryController : Controller
    {
        private readonly ISender _sender;
        public CategoryController(ISender sender)
        {
            _sender = sender;
        }
        [Authorize]
        [HttpGet("Category/select-list")]
        public async Task<IActionResult> Categoryselectlist( CancellationToken cancellationToken)
        {
            var query = new CategorySelectedListQuery();
            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}
