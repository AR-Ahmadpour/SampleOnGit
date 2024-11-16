using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using SharedKernel;

namespace Accreditation.Application.Standards.LogicalDelete
{
    internal sealed class LogicalDeleteStandardCommandHandler :
        ICommandHandler<LogicalDeleteStandardCommand>
    {
        private readonly IStandardRepository _standardRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogicalDeleteStandardCommandHandler(IStandardRepository standardRepository,
                                                   IUnitOfWork unitOfWork)
        {
            _standardRepository = standardRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(
            LogicalDeleteStandardCommand command,
            CancellationToken cancellationToken)
        {
            var standard = await _standardRepository.FindAsync(command.GUID, cancellationToken);
            if (standard is null)
            {
                return Result.Failure<Guid>(StandardErrors.NotFound);
            }

            standard.LogicalDelete(standard.IsDeleted);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
