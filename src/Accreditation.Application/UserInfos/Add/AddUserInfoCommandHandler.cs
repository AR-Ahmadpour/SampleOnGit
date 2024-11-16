using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Sanjehs;
using Accreditation.Application.Common.Interfaces.Persistence.UserInfos;
using Accreditation.Domain.Mehvars.Entities;
using Accreditation.Domain.Sanjehs.Entities;
using Accreditation.Domain.UserInfos.Entities;
using Accreditation.Domain.ZirMehvars.Entities;
using SharedKernel;

namespace Accreditation.Application.UserInfos.Add
{
    internal sealed class AddUserInfoCommandHandler :
        ICommandHandler<AddUserInfoCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfoRepository _userInfoRepository;

        public AddUserInfoCommandHandler(IUnitOfWork unitOfWork, IUserInfoRepository userInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<Result<Guid>> Handle(AddUserInfoCommand request, CancellationToken cancellationToken)
        {

            var userInfo = UserInfo.Create(request.MaritalStatus,request.ChildCount,request.BirthOstandId,request.BirthShahrId,
                request.AddressOstanId,request.AddressShahrId,request.Address,request.PersonalPicGUID,request.KartMeliGUID,request.ShenasnamehGUID,
                request.UserGUID);


            _userInfoRepository.Add(userInfo);


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userInfo.GUID;
        }
    }
}
