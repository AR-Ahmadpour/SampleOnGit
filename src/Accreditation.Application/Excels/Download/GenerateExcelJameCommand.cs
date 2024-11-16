using Accreditation.Application.Body.GetByAccreditationInstanceGuid;
using Accreditation.Application.Headers.GetList;
using MediatR;
using SharedKernel;

namespace Accreditation.Application.Excels
{
    public sealed record GenerateExcelJameCommand
        (GetHeaderDto Header,GetBodyByAccreditationInstanceGuidDto Body) : IRequest<Result<(byte[] FileContent, string FileName)>>;

}
