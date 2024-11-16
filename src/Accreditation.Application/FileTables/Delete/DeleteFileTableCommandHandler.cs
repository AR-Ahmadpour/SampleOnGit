using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.FileTables;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.FileTables.Delete
{
    internal class DeleteFileTableCommandHandler:ICommandHandler<DeleteFileTableCommand>
    {
        private readonly IFileTableRepository _fileTableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFileTableCommandHandler(IFileTableRepository fileTableRepository, IUnitOfWork unitOfWork)
        {
            _fileTableRepository = fileTableRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteFileTableCommand request, CancellationToken cancellationToken)
        {
           var fileTable = await  _fileTableRepository.FindByName(request.FileName, cancellationToken);
            if (fileTable == null) 
            {
                return Result.Failure(FileTableErrors.DeleteError);
            }
            _fileTableRepository.Delete(fileTable, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
