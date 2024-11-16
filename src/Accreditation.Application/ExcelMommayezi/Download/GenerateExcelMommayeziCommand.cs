using Accreditation.Application.Body.GetByAccreditationInstanceGuid;
using Accreditation.Application.Headers.GetList;
using MediatR;
using SharedKernel;


namespace Accreditation.Application.ExcelMommayezi.Download
{
    public sealed record GenerateExcelMommayeziCommand
           (GetHeaderDto Header, GetBodyByAccreditationInstanceGuidMommayeziDto Body) : IRequest<Result<(byte[] FileContent, string FileName)>>;
}
