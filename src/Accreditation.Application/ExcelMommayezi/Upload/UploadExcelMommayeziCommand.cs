using MediatR;
using SharedKernel;

namespace Accreditation.Application.ExcelMommayezi.Upload
{
    public sealed record UploadExcelMommayeziCommand(byte[] FileContent, Guid UserGuid, Guid AccInstanceGuid, Guid FieldGuid) : IRequest<Result>;
}
