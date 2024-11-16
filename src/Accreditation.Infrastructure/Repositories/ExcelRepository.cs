using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Common.Interfaces.Persistence.Excel;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Accreditation.Infrastructure.Repositories
{
    internal sealed class ExcelRepository : IExcelRepository
    {

        private readonly AccreditationDbContext context;
        private readonly IUnitOfWork unitOfWork;


        public ExcelRepository(AccreditationDbContext context, IUnitOfWork unitOfWork)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            this.context = context;
            this.unitOfWork = unitOfWork;
        }

        public async Task ProcessExcelFileAsync(byte[] fileContent, CancellationToken cancellationToken)
        {
            try
            {
                using var package = new ExcelPackage(new MemoryStream(fileContent));
                var worksheet = package.Workbook.Worksheets[0];

                // Start at row 5 and loop through column F
                for (int rowF = 5; rowF <= worksheet.Dimension.End.Row; rowF++)
                {
                    var valueCellF = worksheet.Cells[rowF, 6].Text;
                    var guidCellN = worksheet.Cells[rowF, 14].Text;

                    // Exit the loop if there's no value in column F
                    if (string.IsNullOrEmpty(valueCellF))
                        break;

                    // Parse GUID from column N
                    if (Guid.TryParse(guidCellN, out Guid guid))
                    {
                        // Fetch the corresponding AccreditationInstanceAnswer using the parsed GUID
                        var accreditationInstanceAnswer = await context.AccreditationalInstanceAnswers
                            .FirstOrDefaultAsync(aia => aia.GUID == guid, cancellationToken);

                        if (accreditationInstanceAnswer != null)
                        {
                            for (int rowO = 1; rowO <= worksheet.Dimension.End.Row; rowO++)
                            {
                                var valueCellO = worksheet.Cells[rowO, 15].Text; // Column O (1-based index 15)

                                if (valueCellO == valueCellF)
                                {

                                    var valueCellP = worksheet.Cells[rowO, 16].Text; // Column P (1-based index 16)

                                    // Try parsing the value from column P into a decimal
                                    if (decimal.TryParse(valueCellP, out decimal resultValue))
                                    {
                                        // Update the AccreditationInstanceAnswer with the ResultValue from column P
                                        accreditationInstanceAnswer.EditResult(resultValue);

                                    }

                                    // Exit the loop once the value is found and processed
                                    break;
                                }
                            }
                        }
                    }
                }

                await context.SaveChangesAsync(cancellationToken);




            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new ApplicationException("Error processing Excel file", ex);
            }
        }

        public async Task ProcessExcelMommayeziFileAsync(byte[] fileContent, CancellationToken cancellationToken)
        {
            try
            {
                using var package = new ExcelPackage(new MemoryStream(fileContent));
                var worksheet = package.Workbook.Worksheets[0];


                for (int rowG = 5; rowG <= worksheet.Dimension.End.Row; rowG++)
                {
                    var valueCellG = worksheet.Cells[rowG, 7].Text; // Column G (1-based index 7)
                    var guidCellN = worksheet.Cells[rowG, 14].Text; // Column N (1-based index 14)

                    // Exit the loop if there's no value in column G
                    if (string.IsNullOrEmpty(valueCellG))
                        break;

                    // Parse GUID from column N
                    if (Guid.TryParse(guidCellN, out Guid guid))
                    {
                        // Fetch the corresponding AccreditationInstanceAnswer using the parsed GUID
                        var accreditationInstanceAnswer = await context.AccreditationalInstanceAnswers
                            .FirstOrDefaultAsync(aia => aia.GUID == guid, cancellationToken);

                        if (accreditationInstanceAnswer != null)
                        {
                            for (int rowO = 1; rowO <= worksheet.Dimension.End.Row; rowO++)
                            {
                                var valueCellO = worksheet.Cells[rowO, 15].Text; // Column O (1-based index 15)

                                if (valueCellO == valueCellG)
                                {
                                    var valueCellP = worksheet.Cells[rowO, 16].Text; // Column P (1-based index 16)

                                    // Try parsing the value from column P into a decimal
                                    if (decimal.TryParse(valueCellP, out decimal resultValue))
                                    {
                                        // Update the AccreditationInstanceAnswer with the ResultValue from column P
                                        accreditationInstanceAnswer.EditAuditResult(resultValue);
                                    }

                                    // Exit the loop once the value is found and processed
                                    break;
                                }
                            }
                        }
                    }
                }

                await context.SaveChangesAsync(cancellationToken);









            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error processing Excel file", ex);
            }
        }
    }
}
