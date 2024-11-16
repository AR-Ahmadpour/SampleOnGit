using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Sanjehs;
using Accreditation.Application.OrgGerayesh;
using Accreditation.Application.Sanjehs;
using Accreditation.Domain.OrgGerayesh.Abstractions;
using SharedKernel;
using Accreditation.Domain.NotNaSanjehs.Entities;
using Accreditation.Application.Common.Interfaces.Persistence.NotNaSanjehs;
using Accreditation.Application.Common.Interfaces.Services; 

namespace Accreditation.Application.NotNaSanjehs.Add
{
    internal sealed class AddNotNaSanjehCommandHandler :
        ICommandHandler<AddNotNaSanjehCommand>
    {
        private readonly ISanjehRepository _sanjehRepository;
        private readonly IOrgGerayeshRepository _orgGerayeshRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotNaSanjehRepository _notNaSanjehRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ICurrentUser _userContext;


        public AddNotNaSanjehCommandHandler(
            ISanjehRepository sanjehRepository,
            IOrgGerayeshRepository orgGerayeshRepository,
            IUnitOfWork unitOfWork,
            INotNaSanjehRepository notNaSanjehRepository,
            IDateTimeProvider dateTimeProvider,
            ICurrentUser userContext)

        {
            _sanjehRepository = sanjehRepository;
            _orgGerayeshRepository = orgGerayeshRepository;
            _unitOfWork = unitOfWork;
            _notNaSanjehRepository = notNaSanjehRepository;
            _dateTimeProvider = dateTimeProvider;
            _userContext = userContext;
        }

        public async Task<Result> Handle(AddNotNaSanjehCommand request, CancellationToken cancellationToken)
        {
            if (await _sanjehRepository.FindAsync(request.SanjehGuid, cancellationToken) is null)
            {
                return Result.Failure(SanjehErrors.NotFoundSanjeh);
            }

            foreach (var orgGerayeshGuid in request.OrgGerayeshGuids)
            {
                if (!await _orgGerayeshRepository.FindAsync(orgGerayeshGuid, cancellationToken))
                {
                    return Result.Failure(OrgGerayeshErrors.NotFound);
                }
            }



            //var existingOrgGerayeshGuids = await _notNaSanjehRepository.GetOrgGerayeshGuidsBySanjehId(request.SanjehGuid, cancellationToken);


            //var orgGerayeshGuidsToAdd = request.OrgGerayeshGuids
            //              .Where(guid => !existingOrgGerayeshGuids.Contains(guid))
            //              .ToList();


            //if (!orgGerayeshGuidsToAdd.Any())
            //{

            //    return Result.Failure(NotNaSanjehErrors.AllOrgGerayeshAlreadyAssigned);
            //}



            var existingNotNaSanjehEntries = await _notNaSanjehRepository.GetBySanjehGuidAsync(request.SanjehGuid, cancellationToken);
            if (existingNotNaSanjehEntries.Any())
            {
                _notNaSanjehRepository.RemoveRange(existingNotNaSanjehEntries);
            }

            foreach (var orgGerayesh in request.OrgGerayeshGuids)
            {
                var notNaSanjeh = NotNaSanjeh.Create(request.SanjehGuid, orgGerayesh, Guid.Parse(_userContext.UserId),
                    _dateTimeProvider.Now);

                _notNaSanjehRepository.Add(notNaSanjeh);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
