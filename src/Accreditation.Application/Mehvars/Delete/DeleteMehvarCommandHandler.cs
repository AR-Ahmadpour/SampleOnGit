
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Domain.Mehvars.Entities;
using SharedKernel;

namespace Accreditation.Application.Mehvars.Delete
{
    internal sealed class DeleteMehvarCommandHandler
    : ICommandHandler<DeleteMehvarCommand>
    {
        private readonly IMehvarRepository _mehvarRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMehvarCommandHandler(IMehvarRepository mehvarRepository,
                                          IUnitOfWork unitOfWork)
        {
            _mehvarRepository = mehvarRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(
            DeleteMehvarCommand command,
            CancellationToken cancellationToken)
        {
            if (!await _mehvarRepository.AnyAsync(command.GUID, cancellationToken))
            {
                return Result.Failure(MehvarErrors.NotFound);
            }

            var mehvar = Mehvar.CreateById(command.GUID);

            _mehvarRepository.Delete(mehvar);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
