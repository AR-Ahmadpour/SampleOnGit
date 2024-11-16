using MediatR;
using SharedKernel;

namespace Accreditation.Application.Excels.Upload
{
    public sealed record UploadExcelCommand(byte[] FileContent, Guid UserGuid, Guid AccInstanceGuid, Guid FieldGuid) : IRequest<Result>;

}
