using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.EtebarDorehs;
using Accreditation.Domain.Mehvars.Entities;
using SharedKernel;

namespace Accreditation.Application.Mehvars.Add;

internal sealed class AddMehvarCommandHandler :
    ICommandHandler<AddMehvarCommand, Guid>
{
    private readonly IMehvarRepository _mehvarRepository;
    private readonly ICurrentUser _userContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEtebarDorehRepository _etebarDorehRepository;

    public AddMehvarCommandHandler(IMehvarRepository mehvarRepository, ICurrentUser userContext,
                                   IDateTimeProvider dateTimeProvider, IUnitOfWork unitOfWork,
                                   IEtebarDorehRepository etebarDorehRepository)
    {
        _mehvarRepository = mehvarRepository;
        _userContext = userContext;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _etebarDorehRepository = etebarDorehRepository;
    }
    public async Task<Result<Guid>> Handle(
        AddMehvarCommand command,
        CancellationToken cancellationToken)
    {
        if (await _etebarDorehRepository.FindAsync(command.EtebarDorehGUID, cancellationToken) is null)
        {
            return Result.Failure<Guid>(EtebarDorehErrors.NotFound);
        }

        if (!await _mehvarRepository.IsTitleUniqueAsync(null, command.EtebarDorehGUID, command.Title, cancellationToken))
        {
            return Result.Failure<Guid>(MehvarErrors.TitleNotUnique);
        }

        var mehvar = Mehvar.Create(command.EtebarDorehGUID, command.Title,
            command.SortOrder, Guid.Parse(_userContext.UserId),
        _dateTimeProvider.Now, command.WeightedCoefficient);
        _mehvarRepository.Add(mehvar);


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return mehvar.GUID;
    }
}
