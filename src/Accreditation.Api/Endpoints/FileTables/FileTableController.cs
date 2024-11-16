using Accreditation.Application.FileTables.Add;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;
using Accreditation.Application.FileTables.Get;
using Accreditation.Application.FileTables.Delete;

namespace Accreditation.Api.Endpoints.FileTables
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class FileTableController : Controller
    {
        private readonly ISender _sender;

        public FileTableController(ISender sender)
        {
            _sender = sender;
        }



        //[HttpPost("Upload")]
        //public async Task<IActionResult> Upload(IFormFile file)
        //{
        //    var fileName = Guid.NewGuid() + file.FileName[file.FileName.LastIndexOf('.')..];
        //    var stream = file.OpenReadStream();

        //    await databaseContext.AccDocument.CreateAsync(fileName, stream);

        //    return Ok(fileName);
        //}


        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("فایلی انتخاب نشده است.");


            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var command = new AddFileTableCommand
            ( 
              Guid.NewGuid() + file.FileName[file.FileName.LastIndexOf('.')..],  //file.FileName,
              memoryStream.ToArray(),  //await ReadFileStream(file),
              DateTime.UtcNow,
              DateTime.UtcNow);

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
        [HttpGet("GetFileInfo/{FileName}")]
        public async Task<IActionResult> GetFileInfo(string FileName, CancellationToken cancellationToken)
        {
            //var entity = await databaseContext.AccDocument.Where($"name = '{name}'").FirstOrDefaultAsync();

            //if (entity is null)
            //    return NotFound(nameof(NotFound));
            var request = new GetFileTableByNameQuery(FileName);
            var result = await _sender.Send(request, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
            //return File(result.Value.file!, MediaTypeNames.Application.Octet, result.Value.Name);
        }


        [Authorize]
        [HttpGet("Download/{FileName}")]
        public async Task<IActionResult> Download(string FileName, CancellationToken cancellationToken)
        {
            //var entity = await databaseContext.AccDocument.Where($"name = '{name}'").FirstOrDefaultAsync();

            //if (entity is null)
            //    return NotFound(nameof(NotFound));
            var request = new GetFileTableByNameQuery(FileName);
            var result = await _sender.Send(request, cancellationToken);

            return File(result.Value.file!, MediaTypeNames.Application.Octet, result.Value.Name);
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string name, CancellationToken cancellationToken)
        {
            var request = new DeleteFileTableCommand(name);
            var result = await _sender.Send(request, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }


    }
}
