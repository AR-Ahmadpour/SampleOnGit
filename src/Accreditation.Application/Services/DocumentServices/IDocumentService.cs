using Microsoft.AspNetCore.Http;
using Accrediation.Application.Services.DocumentServices.Dtos;

namespace Accrediation.Application.Services.DocumentServices
{
    public interface IDocumentService
    {
        Task<UploadFileDto> GetDocumentById(Guid id, CancellationToken cancellationToken);
        Task<FileDto> UploadFile(IFormFile file, string userId);
    }
}
