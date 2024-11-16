using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.FileTables;
using Accreditation.Application.EtebarDorehs;
using Accreditation.Domain.FileTables.Entities;
using MediatR;
using Serilog;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.FileTables.Add
{
    public sealed class AddFileTableCommandHandler
        :ICommandHandler<AddFileTableCommand,string>
    {
        private readonly IFileTableRepository _fileTableRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public AddFileTableCommandHandler(IFileTableRepository fileTableRepository, ILogger logger, IUnitOfWork unitOfWork)
        {
            _fileTableRepository = fileTableRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(AddFileTableCommand command ,CancellationToken  cancellationToken)
        {
            try
            {
                var fileTable = FileTable.Create(command.Name, command.File_Stream);
                _fileTableRepository.Add(fileTable);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return Result.Failure<string>(FileTableErrors.AddError);

            }
            return command.Name;
        }
     
    }
}
