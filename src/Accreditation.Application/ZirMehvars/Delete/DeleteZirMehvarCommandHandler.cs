using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Domain.ZirMehvars.Entities;
using SharedKernel;

namespace Accreditation.Application.ZirMehvars.Delete
{
    internal sealed class DeleteZirMehvarCommandHandler
   : ICommandHandler<DeleteZirMehvarCommand>
    {
        private readonly IZirMehvarRepository _zirMehvarRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteZirMehvarCommandHandler(IZirMehvarRepository zirMehvarRepository,
                                             IUnitOfWork unitOfWork)
        {
            _zirMehvarRepository = zirMehvarRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(
            DeleteZirMehvarCommand command,
            CancellationToken cancellationToken)
        {
            if (!await _zirMehvarRepository.AnyAsync(command.GUID, cancellationToken))
            {
                return Result.Failure(ZirMehvarErrors.NotFound);
            }

            var zirMehvar = ZirMehvar.CreateById(command.GUID);

            _zirMehvarRepository.Delete(zirMehvar);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();


        }
    }
}
