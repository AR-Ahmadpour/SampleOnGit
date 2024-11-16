

using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using SharedKernel;

namespace Accreditation.Application.ZirMehvars.LogicalDelete
{
    internal sealed class LogicalDeleteZirMehvarCommandHandler
: ICommandHandler<LogicalDeleteZirMehvarCommand>
    {
        private readonly IZirMehvarRepository _zirMehvarRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogicalDeleteZirMehvarCommandHandler(IZirMehvarRepository zirMehvarRepository,
                                                    IUnitOfWork unitOfWork)
        {
            _zirMehvarRepository = zirMehvarRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(
            LogicalDeleteZirMehvarCommand command,
            CancellationToken cancellationToken)
        {
            var zirMehvar = await _zirMehvarRepository.FindAsync(command.GUID, cancellationToken);
            if (zirMehvar is null)
            {
                return Result.Failure<Guid>(ZirMehvarErrors.NotFound);
            }

            zirMehvar.LogicalDelete(zirMehvar.IsDeleted);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }


    }
}
