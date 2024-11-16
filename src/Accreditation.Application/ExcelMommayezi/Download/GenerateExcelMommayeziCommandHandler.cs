using MediatR;
using OfficeOpenXml;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.ExcelMommayezi.Download
{
    internal sealed class GenerateExcelMommayeziCommandHandler :
        IRequestHandler<GenerateExcelMommayeziCommand, Result<(byte[] FileContent, string FileName)>>
    {
        public GenerateExcelMommayeziCommandHandler()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial for commercial use
        }

        public async Task<Result<(byte[] FileContent, string FileName)>> Handle(GenerateExcelMommayeziCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Set the file name and format it with a timestamp
                string titleCode = "Modiri_Mommayezi";
                string fileName = $"{titleCode}_{DateTime.Now:yyyy-MM-dd}.xlsx";



                using (var memoryStream = new MemoryStream())
                {
                    // Initialize the ExcelPackage
                    using (var package = new ExcelPackage(memoryStream))
                    {

                        var worksheet = package.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == "Arzyabi");

                        if (worksheet == null)
                        {

                            worksheet = package.Workbook.Worksheets.Add("Arzyabi");
                        }
                        else
                        {

                            worksheet.Cells.Clear();
                        }

                        // Merge cells A1:F1
                        worksheet.Cells["A1:G1"].Merge = true;

                        // Set cell A1 value and style
                        worksheet.Cells["A1"].Value = $"اعتبار بخشی موسسات درمانی ایران - {request.Header.EtebarDorehTitle}";
                        worksheet.Cells["A1"].Style.Font.Bold = true;
                        worksheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells["A1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                        // Adjust row height for row 1
                        worksheet.Row(1).Height = 20;

                        // Merge cells A2:F2
                        worksheet.Cells["A2:G2"].Merge = true;

                        // Set cell A2 value and style
                        worksheet.Cells["A2"].Value = $"{request.Header.OrgTypeTitle}, {request.Header.OrganizationName}, {request.Header.ShahrTitle}, {request.Header.UniversityName}  ";
                        worksheet.Cells["A2"].Style.Font.Bold = true;
                        worksheet.Cells["A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells["A2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells["A2"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                        // Adjust row height for row 2
                        worksheet.Row(2).Height = 20;

                        // Add headers in row 3
                        worksheet.Cells["A3:G3"].Merge = true;
                        worksheet.Cells["A3"].Value = $"ارزیاب : {request.Header.ArzyabCredentials} - بسته ارزیابی : {request.Header.FieldTitle}";
                        worksheet.Cells["A3"].Style.Font.Bold = true;
                        worksheet.Cells["A3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells["A3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells["A3"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue); // Same color as rows 1 and 2

                        // Set row height for row 3 to be the same as rows 1 and 2
                        worksheet.Row(3).Height = 20;

                        // Add headers in row 4
                        worksheet.Cells["A4"].Value = "محور| زیر محور |استاندارد";
                        worksheet.Cells["B4"].Value = "سنجه";
                        worksheet.Cells["C4"].Value = "پاسخ بیمارستان";
                        worksheet.Cells["D4"].Value = "نظر نهایی دانشگاه و وزارت";
                        worksheet.Cells["E4"].Value = "آخرین ادواری";
                        worksheet.Cells["F4"].Value = "نظر ارزیاب";
                        worksheet.Cells["G4"].Value = "نظر ارزیاب ارشد";

                        // Apply style for row 4 (same as row 1)
                        worksheet.Cells["A4:G4"].Style.Font.Bold = true;
                        worksheet.Cells["A4:G4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A4:G4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells["A4:G4"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells["A4:G4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(124, 152, 218));

                        // Enable text wrapping for columns C and D
                        worksheet.Cells["C4"].Style.WrapText = true;
                        worksheet.Cells["D4"].Style.WrapText = true;
                        worksheet.Cells["E4"].Style.WrapText = true;
                        worksheet.Cells["G4"].Style.WrapText = true;

                        // Set row height for row 4 to accommodate wrapping
                        worksheet.Row(4).Height = 70;

                        // Adjust the column widths (wider for A and B, normal for others)
                        worksheet.Column(1).Width = 40;
                        worksheet.Column(2).Width = 70;
                        worksheet.Column(3).Width = 15;
                        worksheet.Column(4).Width = 15;
                        worksheet.Column(5).Width = 10;
                        worksheet.Column(6).Width = 10;
                        worksheet.Column(7).Width = 10;

                        // worksheet to RTL
                        worksheet.View.RightToLeft = true;



                        // Handling Mehvars, ZirMehvars, and Standards

                        int startRowMehvar = 5; // Start writing from row 5
                        int mergeStartRow = startRowMehvar; // Track where to start the merge

                        string previousStandard = null; // To track the last standard
                        bool applyColor = false; // Flag to alternate between coloring and not coloring

                        for (int i = 0; i < request.Body.Standards.Count; i++)
                        {
                            string currentMehvar = request.Body.Mehvars.ElementAtOrDefault(i) ?? string.Empty;
                            string currentZirMehvar = request.Body.ZirMehvars.ElementAtOrDefault(i) ?? string.Empty;
                            string currentStandard = request.Body.Standards.ElementAtOrDefault(i) ?? string.Empty;

                            if (currentStandard != previousStandard && previousStandard != null)
                            {
                                // Merge cells for the previous standard group
                                worksheet.Cells[$"A{mergeStartRow}:A{startRowMehvar - 1}"].Merge = true;
                                worksheet.Cells[$"A{mergeStartRow}"].Value = $"محور: {request.Body.Mehvars.ElementAtOrDefault(mergeStartRow - 5)}, زیر محور: {request.Body.ZirMehvars.ElementAtOrDefault(mergeStartRow - 5)}, استاندارد: {previousStandard}";

                                // Apply text wrapping, centering, and styles for merged cells
                                worksheet.Cells[$"A{mergeStartRow}"].Style.WrapText = true;
                                worksheet.Cells[$"A{mergeStartRow}"].Style.Font.Bold = true;
                                worksheet.Cells[$"A{mergeStartRow}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells[$"A{mergeStartRow}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                                // Apply alternating colors
                                if (applyColor)
                                {
                                    worksheet.Cells[$"A{mergeStartRow}:A{startRowMehvar - 1}"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                    worksheet.Cells[$"A{mergeStartRow}:A{startRowMehvar - 1}"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue); // Example color
                                }

                                // Toggle color application for the next group
                                applyColor = !applyColor;

                                // Start a new merge group for the new standard
                                mergeStartRow = startRowMehvar;
                            }

                            // Update previousStandard to current one for next iteration
                            previousStandard = currentStandard;

                            startRowMehvar++; // Move to the next row
                        }

                        // Handle the last group of standards
                        worksheet.Cells[$"A{mergeStartRow}:A{startRowMehvar - 1}"].Merge = true;
                        worksheet.Cells[$"A{mergeStartRow}"].Value = $"محور: {request.Body.Mehvars.ElementAtOrDefault(mergeStartRow - 5)}, زیر محور: {request.Body.ZirMehvars.ElementAtOrDefault(mergeStartRow - 5)}, استاندارد: {previousStandard}";

                        // Apply text wrapping, centering, and styles for the final merged cells
                        worksheet.Cells[$"A{mergeStartRow}"].Style.WrapText = true;
                        worksheet.Cells[$"A{mergeStartRow}"].Style.Font.Bold = true;
                        worksheet.Cells[$"A{mergeStartRow}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[$"A{mergeStartRow}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                        // Apply alternating colors for the last group
                        if (applyColor)
                        {
                            worksheet.Cells[$"A{mergeStartRow}:A{startRowMehvar - 1}"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[$"A{mergeStartRow}:A{startRowMehvar - 1}"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue); // Example color
                        }




                        // dealing with sanjeh
                        int startRowSanjeh = 5;
                        Color lightBlueColor = Color.LightBlue;

                        foreach (var sanjeh in request.Body.Sanjehs)
                        {
                            worksheet.Cells[$"B{startRowSanjeh}"].Value = sanjeh;

                            if (startRowSanjeh % 2 != 0)
                            {
                                worksheet.Cells[$"B{startRowSanjeh}"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                worksheet.Cells[$"B{startRowSanjeh}"].Style.Fill.BackgroundColor.SetColor(lightBlueColor);
                            }

                            worksheet.Row(startRowSanjeh).Height = 40; // Adjust as needed based on your requirement


                            worksheet.Cells[$"B{startRowSanjeh}"].Style.WrapText = true; // wrap the sanjeh texts

                            startRowSanjeh++;
                        }




                        //done with result father
                        int startRowResultFather = 5;


                        foreach (var resultfather in request.Body.ResultFather)
                        {
                            worksheet.Cells[$"C{startRowResultFather}"].Value = resultfather;



                            worksheet.Row(startRowResultFather).Height = 40; // Adjust as needed based on your requirement


                            worksheet.Cells[$"C{startRowResultFather}"].Style.WrapText = true; // wrap the sanjeh texts
                            worksheet.Cells[$"C{startRowResultFather}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Center horizontally
                            worksheet.Cells[$"C{startRowResultFather}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; // Center vertically

                            startRowResultFather++;
                        }


                        //dealing with universityfinal answer
                        int startRowUniversityAnswer = 5;

                        foreach (var answer in request.Body.FinalUniversityAnswer)
                        {
                            // Initialize with a default value
                            string valueToWrite = "بدون نظر"; // Default to "No opinion"

                            // Determine the value to write based on the nullable bool (answer)
                            if (answer == false) // 0 or false
                            {
                                valueToWrite = "مخالف"; // Opposed
                            }
                            else if (answer == true) // 1 or true
                            {
                                valueToWrite = "موافق"; // In favor
                            }

                            // Write the value to the Excel cell
                            worksheet.Cells[$"D{startRowUniversityAnswer}"].Value = valueToWrite;

                            // Set row height and style
                            worksheet.Row(startRowUniversityAnswer).Height = 40; // Adjust as needed
                            worksheet.Cells[$"D{startRowUniversityAnswer}"].Style.WrapText = true; // Wrap the text

                            worksheet.Cells[$"D{startRowUniversityAnswer}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Center horizontally
                            worksheet.Cells[$"D{startRowUniversityAnswer}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; // Center vertically

                            startRowUniversityAnswer++;
                        }



                        //dealing with finalPeriod
                        var StartRowFinalPeriod = 5;
                        foreach (var finalperiod in request.Body.FinalPeriod)
                        {
                            worksheet.Cells[$"E{StartRowFinalPeriod}"].Value = finalperiod;


                            worksheet.Row(StartRowFinalPeriod).Height = 40; // Adjust as needed based on your requirement
                            worksheet.Cells[$"E{StartRowFinalPeriod}"].Style.WrapText = true; // wrap the sanjeh texts

                            worksheet.Cells[$"E{StartRowFinalPeriod}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Center horizontally
                            worksheet.Cells[$"E{StartRowFinalPeriod}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; // Center vertically

                            StartRowFinalPeriod++;
                        }


                        // Add values to column O from row 1 to row 12
                        for (int row = 1; row <= request.Body.ResultForColumnO.Count; row++)
                        {
                            // Get the value from the NaLists and assign it to the cell
                            string value = request.Body.ResultForColumnO[row - 1]; // Directly get the value (0-10 or "NA")

                            // Set the value in the worksheet, starting from O1
                            worksheet.Cells[$"O{row}"].Value = value;
                        }

                        worksheet.Column(15).Width = 0; // Column O is the 15th column


                        for (int row = 1; row <= request.Body.ResultForDb.Count; row++)
                        {
                            // Get the value from ResultValue and assign it to the cell
                            decimal valueP = request.Body.ResultForDb[row - 1]; // Get the value for column P

                            // Set the value in the worksheet, starting from P1 (column P is the 16th column)
                            worksheet.Cells[$"P{row}"].Value = valueP;
                        }
                        worksheet.Column(16).Width = 0;

                        worksheet.Cells["N4"].Value = "PK نظر ارزیاب ارشد";
                        worksheet.Cells["N4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Center text for N4

                        // Fill column N starting from N5 with PkOfResultChild values from the DTO
                        for (int row = 5; row <= request.Body.PkOfResultChild.Count + 4; row++) // Start at N5, hence the +4
                        {
                            var pkValue = request.Body.PkOfResultChild[row - 5]; // Adjust for the correct index (row-5 to match the 0-based index)

                            // Set the value in the worksheet for column N
                            worksheet.Cells[$"N{row}"].Value = pkValue;
                        }

                        worksheet.Column(14).Width = 0;

                        var evaluatorCell = worksheet.Cells["M4"];
                        evaluatorCell.Value = $"ارزیاب : {request.Header.UserGuid}";
                        evaluatorCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Center text

                        var organizationCell = worksheet.Cells["M5"];
                        organizationCell.Value = $"سازمان : {request.Header.OrganizationGuid}";
                        organizationCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Center text

                        var fieldCell = worksheet.Cells["M6"];
                        fieldCell.Value = $"بسته : {request.Header.FieldGuid}";
                        fieldCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Center text

                        var accreditationInstanceCell = worksheet.Cells["M7"];
                        accreditationInstanceCell.Value = $"نمونه اعتبار بخشی : {request.Header.AccreditationInstanceGuid}";
                        accreditationInstanceCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Center text

                        // Optionally, adjust column width for column M if needed
                        worksheet.Column(13).Width = 0; // Column M is the 13th column




                        /* *************************************************************************************************
                        //for (int row = 5; row < 5 + request.Body.NaLists.Count; row++)
                        //{
                        //    var cellAddress = $"G{row}";

                        //    worksheet.Cells[cellAddress].Style.Locked = false;

                        //    // Remove existing data validations for the current cell
                        //    var existingValidations = worksheet.DataValidations
                        //        .Where(v => v.Address.Address == cellAddress)
                        //        .ToList();
                        //    foreach (var validationn in existingValidations)
                        //    {
                        //        worksheet.DataValidations.Remove(validationn);
                        //    }

                        //    // Add data validation for the dropdown list
                        //    var validation = worksheet.DataValidations.AddListValidation(cellAddress);

                        //    // Check the corresponding boolean value in NaLists for the Sanjeh of this row
                        //    bool isNotNa = request.Body.NaLists[row - 5]; // Assuming NaLists contains boolean values (true = no NA, false = include NA)

                        //    // Set the formula for the dropdown
                        //    if (isNotNa)
                        //    {
                        //        // If true (no NA), use the range O1:O11
                        //        validation.Formula.ExcelFormula = "=O1:O11";
                        //    }
                        //    else
                        //    {
                        //        // If false (include NA), use the range O1:O12
                        //        validation.Formula.ExcelFormula = "=O1:O12";
                        //    }

                        //    // Show error message if invalid entry is made
                        //    validation.ShowErrorMessage = true;
                        //    validation.Error = "Please select a value from the dropdown";
                        //    validation.ErrorTitle = "Error";
                        //    validation.ErrorStyle = OfficeOpenXml.DataValidation.ExcelDataValidationWarningStyle.stop; // Use stop as error style
                        //}

                        */ 



                        // dealing with arzyab's answer
                        var startRowArzyabAnswer = 5;
                        foreach (var arzyabAnswer in request.Body.ResultChild)
                        {
                            var cellAddress = $"F{startRowArzyabAnswer}";
                            var cell = worksheet.Cells[cellAddress];

                            // Set the cell value
                            cell.Value = arzyabAnswer;

                            // Adjust row height and enable text wrapping
                            worksheet.Row(startRowArzyabAnswer).Height = 40;
                            cell.Style.WrapText = true;

                            // Center align text horizontally and vertically
                            cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            cell.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            // Apply background color based on cell value
                            if (arzyabAnswer == "NA")
                            {
                                // Color NA cells in yellow
                                cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                            }
                            else if (int.TryParse(arzyabAnswer, out int number) && number >= 0 && number <= 10)
                            {
                                // Apply gradient color for numbers 0 to 10
                                cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                                // Define the start and end colors
                                var startColor = System.Drawing.Color.FromArgb(46, 217, 89); // For 0 (light green)
                                var endColor = System.Drawing.Color.FromArgb(10, 112, 36);   // For 10 (darker green)

                                // Interpolate between startColor and endColor based on the number
                                float fraction = number / 10f;

                                int red = (int)(startColor.R + (endColor.R - startColor.R) * fraction);
                                int green = (int)(startColor.G + (endColor.G - startColor.G) * fraction);
                                int blue = (int)(startColor.B + (endColor.B - startColor.B) * fraction);

                                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(red, green, blue));
                            }

                            startRowArzyabAnswer++;
                        }

                        for (int row = 5; row < 5 + request.Body.AuditResult.Count; row++)
                        {
                            var cellAddressF = $"F{row}";
                            var cellAddressG = $"G{row}";

                            var cellF = worksheet.Cells[cellAddressF];
                            var cellG = worksheet.Cells[cellAddressG];

                            // Get the corresponding value from AuditResult
                            var auditResultValue = request.Body.AuditResult[row - 5]; // Assuming AuditResult contains the values for column G

                            // If the AuditResult value is null or empty, use the value from the corresponding F cell
                            if (auditResultValue == null || string.IsNullOrEmpty(auditResultValue.ToString()))
                            {
                                cellG.Value = cellF.Value; // Copy value from column F to G
                            }
                            else
                            {
                                // If not null, set the value from AuditResult
                                cellG.Value = auditResultValue;
                            }

                            // Set text alignment for column G
                            worksheet.Row(row).Height = 40; // Adjust row height
                            cellG.Style.WrapText = true;
                            cellG.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            cellG.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            // Apply background color based on cell value
                            if (cellG.Value.ToString() == "NA")
                            {
                                // Color NA cells in yellow
                                cellG.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                cellG.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                            }
                            else if (int.TryParse(cellG.Value.ToString(), out int number) && number >= 0 && number <= 10)
                            {
                                // Apply gradient color for numbers 0 to 10
                                cellG.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                                // Define the start and end colors
                                var startColor = System.Drawing.Color.FromArgb(46, 217, 89); // For 0 (light green)
                                var endColor = System.Drawing.Color.FromArgb(10, 112, 36);   // For 10 (darker green)

                                // Interpolate between startColor and endColor based on the number
                                float fraction = number / 10f;

                                int red = (int)(startColor.R + (endColor.R - startColor.R) * fraction);
                                int green = (int)(startColor.G + (endColor.G - startColor.G) * fraction);
                                int blue = (int)(startColor.B + (endColor.B - startColor.B) * fraction);

                                cellG.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(red, green, blue));
                            }

                            // Continue with unlocking the cell and handling dropdowns for column G
                            worksheet.Cells[cellAddressG].Style.Locked = false;

                            // Remove existing data validations for the current G cell
                            var existingValidations = worksheet.DataValidations
                                .Where(v => v.Address.Address == cellAddressG)
                                .ToList();
                            foreach (var validationn in existingValidations)
                            {
                                worksheet.DataValidations.Remove(validationn);
                            }

                            // Add data validation for the dropdown list
                            var validation = worksheet.DataValidations.AddListValidation(cellAddressG);

                            // Check the corresponding boolean value in NaLists for the Sanjeh of this row
                            bool isNotNa = request.Body.NaLists[row - 5]; // Assuming NaLists contains boolean values (true = no NA, false = include NA)

                            // Set the formula for the dropdown
                            if (isNotNa)
                            {
                                // If true (no NA), use the range O1:O11
                                validation.Formula.ExcelFormula = "=O1:O11";
                            }
                            else
                            {
                                // If false (include NA), use the range O1:O12
                                validation.Formula.ExcelFormula = "=O1:O12";
                            }

                            // Show error message if invalid entry is made
                            validation.ShowErrorMessage = true;
                            validation.Error = "Please select a value from the dropdown";
                            validation.ErrorTitle = "Error";
                            validation.ErrorStyle = OfficeOpenXml.DataValidation.ExcelDataValidationWarningStyle.stop; // Use stop as error style
                        }






                        worksheet.Protection.IsProtected = true;
                        worksheet.Protection.AllowSelectLockedCells = true; // Optional: Disallow selecting locked cells
                        worksheet.Protection.AllowSelectUnlockedCells = true;

                        // Save the package
                        await package.SaveAsync(cancellationToken);
                    }

                    // Read the file back into a byte array to return as a downloadable file
                    byte[] fileBytes = memoryStream.ToArray();

                    // Return the file as a byte array
                    return Result.Success((fileBytes, fileName));
                }
            }
            catch (Exception ex)
            {
                var excelError = new Error("ExcelGenerationError", $"An error occurred while generating the Excel file: {ex.Message}", ErrorType.Failure);
                return Result.Failure<(byte[] FileContent, string FileName)>(excelError);
            }
        }
    }
    
}
