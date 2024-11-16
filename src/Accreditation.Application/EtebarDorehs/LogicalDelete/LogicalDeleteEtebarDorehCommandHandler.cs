using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using SharedKernel;

namespace Accreditation.Application.EtebarDorehs.LogicalDelete;

internal sealed class LogicalDeleteEtebarDorehCommandHandler
: ICommandHandler<LogicalDeleteEtebarDorehCommand>
{
    private readonly IEtebarDorehRepository _etebarDorehRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LogicalDeleteEtebarDorehCommandHandler(IEtebarDorehRepository etebarDorehRepository,
                                                  IUnitOfWork unitOfWork)
    {
        _etebarDorehRepository = etebarDorehRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(
        LogicalDeleteEtebarDorehCommand command,
        CancellationToken cancellationToken)
    {
        var etebarDoreh = await _etebarDorehRepository.FindAsync(command.GUID, cancellationToken);
        if (etebarDoreh is null)
        {
            return Result.Failure<Guid>(EtebarDorehErrors.NotFound);
        }
        if (etebarDoreh.IsCurrent)
        {
            return Result.Failure<Guid>(EtebarDorehErrors.CurrentEtebrDorehCanNotRemoved);
        }

        etebarDoreh.LogicalDelete(etebarDoreh.IsDeleted);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
