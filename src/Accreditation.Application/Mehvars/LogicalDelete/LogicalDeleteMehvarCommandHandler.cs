using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Mehvars;
using SharedKernel;


namespace Accreditation.Application.EtebarDorehs.LogicalDelete;

internal sealed class LogicalDeleteMehvarCommandHandler
: ICommandHandler<LogicalDeleteMehvarCommand>
{
    private readonly IMehvarRepository _mehvarRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LogicalDeleteMehvarCommandHandler(IMehvarRepository mehvarRepository, IUnitOfWork unitOfWork)
    {
        _mehvarRepository = mehvarRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(
        LogicalDeleteMehvarCommand command,
        CancellationToken cancellationToken)
    {
        var mehvar = await _mehvarRepository.FindAsync(command.GUID, cancellationToken);
        if (mehvar is null)
        {
            return Result.Failure<Guid>(MehvarErrors.NotFound);
        }

        mehvar.LogicalDelete(mehvar.IsDeleted);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
