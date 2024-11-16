using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Application.Common.Interfaces.Services;
using Accreditation.Application.Mehvars;
using Accreditation.Domain.ZirMehvars.Entities;
using SharedKernel;

namespace Accreditation.Application.ZirMehvars.Add
{
    public sealed class AddZirMehvarCommandHandler : ICommandHandler<AddZirMehvarCommand, Guid>
    {
        private readonly IZirMehvarRepository _zirMehvarRepository;
        private readonly ICurrentUser _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMehvarRepository _mehvarRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddZirMehvarCommandHandler(IZirMehvarRepository zirMehvarRepository,
                                          ICurrentUser userContext,
                                          IDateTimeProvider dateTimeProvider,
                                          IMehvarRepository mehvarRepository,
                                          IUnitOfWork unitOfWork)
        {
            _zirMehvarRepository = zirMehvarRepository;
            _userContext = userContext;
            _dateTimeProvider = dateTimeProvider;
            _mehvarRepository = mehvarRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(AddZirMehvarCommand command, CancellationToken cancellationToken)
        {

            if (await _mehvarRepository.FindAsync(command.MehvarGuid, cancellationToken) is null)
            {
                return Result.Failure<Guid>(MehvarErrors.NotFound);
            }

            if (!await _zirMehvarRepository.IsTitleUniqueAsync(null, command.MehvarGuid, command.Title, cancellationToken))
            {
                return Result.Failure<Guid>(ZirMehvarErrors.TitleNotUnique);
            }

            var zirMehvar = ZirMehvar.Create(
                command.MehvarGuid,
                command.Title,
                command.SortOrder,
                Guid.Parse(_userContext.UserId),
                _dateTimeProvider.Now, command.WeightedCoefficient);

            _zirMehvarRepository.Add(zirMehvar);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return zirMehvar.GUID;
        }
    }
}
