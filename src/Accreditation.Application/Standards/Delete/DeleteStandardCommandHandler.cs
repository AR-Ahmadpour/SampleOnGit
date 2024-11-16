using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Domain.Standards.Entities;
using SharedKernel;

namespace Accreditation.Application.Standards.Delete
{
    internal sealed class DeleteStandardCommandHandler
     : ICommandHandler<DeleteStandardCommand>
    {
        private readonly IStandardRepository _standardRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStandardCommandHandler(IStandardRepository standardRepository,
                                            IUnitOfWork unitOfWork)
        {
            _standardRepository = standardRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(
            DeleteStandardCommand command,
            CancellationToken cancellationToken)
        {
            if (!await _standardRepository.AnyAsync(command.GUID, cancellationToken))
            {
                return Result.Failure(StandardErrors.NotFound);
            }

            var standard = Standard.CreateById(command.GUID);

            _standardRepository.Delete(standard);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
