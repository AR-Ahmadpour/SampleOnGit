using Accreditation.Application.Common.Interfaces.Persistence.Excel;
using Accreditation.Application.Excels;
using MediatR;
using OfficeOpenXml;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.ExcelMommayezi.Upload
{
    internal sealed class UploadExcelMommayeziCommandHandler :
        IRequestHandler<UploadExcelMommayeziCommand, Result>
    {

        private readonly IExcelRepository _excelRepository;

        public UploadExcelMommayeziCommandHandler(IExcelRepository excelRepository)
        {
            _excelRepository = excelRepository;
        }

        public async Task<Result> Handle(UploadExcelMommayeziCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var package = new ExcelPackage(new MemoryStream(request.FileContent)))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    // Read and validate UserGuid
                    var cellM4 = worksheet.Cells["M4"].Text;
                    var evaluatorPrefix = "ارزیاب :";
                    var evaluatorText = cellM4.StartsWith(evaluatorPrefix)
                        ? cellM4.Substring(evaluatorPrefix.Length).Trim()
                        : cellM4.Trim();
                    if (!Guid.TryParse(evaluatorText, out Guid evaluatorGuid) || evaluatorGuid != request.UserGuid)
                    {
                        return Result.Failure<Guid>(ExcelGlobalErrors.ArzyabError);
                    }

                    // Read and validate FieldGuid
                    var cellM6 = worksheet.Cells["M6"].Text;
                    var fieldPrefix = "بسته :";
                    var fieldText = cellM6.Contains(fieldPrefix)
                        ? cellM6.Substring(cellM6.IndexOf(fieldPrefix) + fieldPrefix.Length).Trim()
                        : cellM6.Trim();
                    if (!Guid.TryParse(fieldText, out Guid fieldGuid) || fieldGuid != request.FieldGuid)
                    {
                        return Result.Failure<Guid>(ExcelGlobalErrors.FieldError);
                    }

                    // Read and validate AccInstanceGuid
                    var cellM7 = worksheet.Cells["M7"].Text;
                    var accInstancePrefix = "نمونه اعتبار بخشی :";
                    var accInstanceText = cellM7.StartsWith(accInstancePrefix)
                        ? cellM7.Substring(accInstancePrefix.Length).Trim()
                        : cellM7.Trim();
                    if (!Guid.TryParse(accInstanceText, out Guid accInstanceGuid) || accInstanceGuid != request.AccInstanceGuid)
                    {
                        return Result.Failure<Guid>(ExcelGlobalErrors.AccINstanceError);
                    }
                }

                // Process the file if validation is successful
                await _excelRepository.ProcessExcelMommayeziFileAsync(request.FileContent, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log it)
                throw new ApplicationException("Error processing Excel file", ex);
            }
        }
    }
}
