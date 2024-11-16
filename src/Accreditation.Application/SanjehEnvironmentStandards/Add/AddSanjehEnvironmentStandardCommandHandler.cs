using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards;
using Accreditation.Application.Common.Interfaces.Persistence.NotNaSanjehs;
using Accreditation.Application.Common.Interfaces.Persistence.SanjehEnvironmentStandards;
using Accreditation.Application.Common.Interfaces.Persistence.Sanjehs;
using Accreditation.Application.EnvironmentStandards;
using Accreditation.Application.Sanjehs;
using Accreditation.Domain.SanjeEnvironemtnStandards.Entities;
using SharedKernel;

namespace Accreditation.Application.SanjehEnvironmentStandards.Add
{
    internal sealed class AddSanjehEnvironmentStandardCommandHandler :
        ICommandHandler<AddSanjehEnvironmentStandardCommand>
    {
        private readonly ISanjehRepository _sanjehRepository;
        private readonly IEnvironmentStandardRepository _environmentStandardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISanjehEnvironmentStandardRepository _sanjehEnvironmentStandardRepository;

        public AddSanjehEnvironmentStandardCommandHandler(ISanjehRepository sanjehRepository, IEnvironmentStandardRepository environmentStandardRepository, IUnitOfWork unitOfWork, ISanjehEnvironmentStandardRepository sanjehEnvironmentStandardRepository)
        {
            _sanjehRepository = sanjehRepository;
            _environmentStandardRepository = environmentStandardRepository;
            _unitOfWork = unitOfWork;
            _sanjehEnvironmentStandardRepository = sanjehEnvironmentStandardRepository;
        }

        public async Task<Result> Handle(AddSanjehEnvironmentStandardCommand request, CancellationToken cancellationToken)
        {

            if (await _sanjehRepository.FindAsync(request.SanjehGuid, cancellationToken) is null)
            {
                return Result.Failure(SanjehErrors.NotFoundSanjeh);
            }

            foreach (var environmentStandardGuid in request.SanjehEnvironmentStandardGuids)
            {
                if (!await _environmentStandardRepository.FindAsync(environmentStandardGuid, cancellationToken))
                {
                    return Result.Failure(EnvironmentStandardErrors.NotFound);
                }
            }


            var existingSanjehEnvironmentStandardEntries = await _sanjehEnvironmentStandardRepository.GetBySanjehGuidAsync(request.SanjehGuid, cancellationToken);
            if (existingSanjehEnvironmentStandardEntries.Any())
            {
                _sanjehEnvironmentStandardRepository.RemoveRange(existingSanjehEnvironmentStandardEntries);
            }






            foreach (var environmentStandardGuid in request.SanjehEnvironmentStandardGuids)
            {
                var sanjehEnvironmentStandard = SanjehEnvironmentStandard.Create(request.SanjehGuid, environmentStandardGuid);

                _sanjehEnvironmentStandardRepository.Add(sanjehEnvironmentStandard);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
