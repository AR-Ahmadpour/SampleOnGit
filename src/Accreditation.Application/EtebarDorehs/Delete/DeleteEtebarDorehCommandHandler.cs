using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Domain.EtebarDorehs.Entities;
using SharedKernel;

namespace Accreditation.Application.EtebarDorehs.Delete;

internal sealed class DeleteEtebarDorehCommandHandler
   : ICommandHandler<DeleteEtebarDorehCommand>
{
    private readonly IEtebarDorehRepository _etebarDorehRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEtebarDorehCommandHandler(IEtebarDorehRepository etebarDorehRepository,
                                           IUnitOfWork unitOfWork)
    {
        _etebarDorehRepository = etebarDorehRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(
        DeleteEtebarDorehCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _etebarDorehRepository.AnyAsync(command.GUID, cancellationToken))
        {
            return Result.Failure(EtebarDorehErrors.NotFound);
        }

        var etebarDoreh = EtebarDoreh.CreateById(command.GUID);

        _etebarDorehRepository.Delete(etebarDoreh);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}