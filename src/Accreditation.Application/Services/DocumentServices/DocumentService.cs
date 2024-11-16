using Accrediation.Application.Common.Errors;
using Accrediation.Application.Common.Interfaces.Services;
using Accrediation.Application.Services.DocumentServices.Dtos;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Accrediation.Application.Services.DocumentServices
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentDetailRepository _documentDetailRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IDocumentUnitOfWork _documentUnitOfWork;

        public DocumentService(
            IDocumentDetailRepository documentDetailRepository,
            IDateTimeProvider dateTimeProvider,
            IDocumentUnitOfWork documentUnitOfWork)
        {
            _documentDetailRepository = documentDetailRepository;
            _dateTimeProvider = dateTimeProvider;
            _documentUnitOfWork = documentUnitOfWork;
        }

        public async Task<FileDto> UploadFile(IFormFile file, string userId)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            var document = new DocumentDetail()
            {
                Id = Guid.NewGuid(),
                FileName = file.FileName.Split(".")[0],
                FileData = await GetFileBytes(file),
                Extension = Path.GetExtension(file.FileName).Split(".")[1],
                UploadDateTime = _dateTimeProvider.UtcNow,
                Status = DocumentStatus.Reserved,
                UserId = userId
            };

            _documentDetailRepository.Add(document);
            await _documentUnitOfWork.Commit();

            return new FileDto { FileExtension = document.Extension, FileId = document.Id };
        }

        public async Task<byte[]> GetFileBytes(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async Task<UploadFileDto?> GetDocumentById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var file = await _documentDetailRepository
                .GetDocumentById(id, cancellationToken);
            if (file == null)
                throw new DocumentNotFoundException();

            return file;
        }
    }
}
