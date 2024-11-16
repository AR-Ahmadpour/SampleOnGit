using Accreditation.Api.Endpoints.Excels.EditRequest;
using Accreditation.Api.Endpoints.Excels.Request;
using Accreditation.Application.ExcelMommayezi.Download;
using Accreditation.Application.ExcelMommayezi.Upload;
using Accreditation.Application.Excels;
using Accreditation.Application.Excels.Upload;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accreditation.Api.Endpoints.Excels
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ExcelController : ControllerBase
    {
        private readonly ISender _sender;

        public ExcelController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost("GenerateExcelJame")]
        public async Task<IActionResult> GenerateExcel([FromBody] ExcelRequest request, CancellationToken cancellationToken)
        {


            var command = new GenerateExcelJameCommand(
                request.Header,
                request.Body
            );

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {

                var (fileContent, fileName) = result.Value;
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(fileContent, contentType, fileName);
            }
            else
            {

                return BadRequest(result.Error);
            }


        }



        [Authorize]
        [HttpPut("UploadExcelJame")]

        public async Task<IActionResult> UploadExcel([FromForm] EditExcelRequest request ,IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("فایلی انتخاب نشده است.");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream, cancellationToken);
            var fileContent = stream.ToArray();

            var command = new UploadExcelCommand(fileContent, request.UserGuid,request.AccInstanceGuid,request.FieldGuid);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok("File processed successfully.");
            }
            else
            {
                return BadRequest(result.Error);
            }
        }





        [Authorize]
        [HttpPost("GenerateExcelMommayezi")]
        public async Task<IActionResult> GenerateExcelMommayezi([FromBody] ExcelMommayeziRequest request, CancellationToken cancellationToken)
        {


            var command = new GenerateExcelMommayeziCommand(
                request.Header,
                request.Body
            );

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {

                var (fileContent, fileName) = result.Value;
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(fileContent, contentType, fileName);
            }
            else
            {

                return BadRequest(result.Error);
            }


        }




        [Authorize]
        [HttpPut("UploadExcelMommayezi")]

        public async Task<IActionResult> UploadExcelMommayezi([FromForm] EditExcelRequest request, IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("فایلی انتخاب نشده است.");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream, cancellationToken);
            var fileContent = stream.ToArray();

            var command = new UploadExcelMommayeziCommand(fileContent, request.UserGuid, request.AccInstanceGuid, request.FieldGuid);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok("File processed successfully.");
            }
            else
            {
                return BadRequest(result.Error);
            }
        }





















    }

}
