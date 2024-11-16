using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.DorehAmoozeshis;
using Accreditation.Application.Common.Interfaces.Persistence.UserDorehs;
using Accreditation.Domain.UserDorehs.Entities;
using SharedKernel;

namespace Accreditation.Application.UserDorehs.Add
{
    internal sealed class AddUserDorehCommandHandler :
        ICommandHandler<AddUserDorehCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDorehAmoozeshiRepository _dorehAmoozeshiRepository;
        private readonly IUserDorehRepository _userDorehRepository;

        public AddUserDorehCommandHandler(
            IUnitOfWork unitOfWork, IDorehAmoozeshiRepository dorehAmoozeshiRepository, IUserDorehRepository userDorehRepository)
        {

            _unitOfWork = unitOfWork;
            _dorehAmoozeshiRepository = dorehAmoozeshiRepository;
            _userDorehRepository = userDorehRepository;
        }

        public async Task<Result<Guid>> Handle(AddUserDorehCommand request, CancellationToken cancellationToken)
        {

            if (await _dorehAmoozeshiRepository.FindAsync(request.DorehAmoozeshiGuid, cancellationToken) is null)
            {
                return Result.Failure<Guid>(UserDorehErrors.DorehAmoozeshiNotFound);
            }



            var userDoreh = UserDoreh.Create(
                request.UserGuid,
                request.DorehAmoozeshiGuid,
                request.DorehTitle,
                request.BargozarKonandeh,
                request.DorehHours,
                request.DorehRole);


            _userDorehRepository.Add(userDoreh);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userDoreh.GUID;

        }
    }
}
