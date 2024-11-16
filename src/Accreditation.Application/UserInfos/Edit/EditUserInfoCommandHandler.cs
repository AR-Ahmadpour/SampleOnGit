using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.UserInfos;
using SharedKernel;

namespace Accreditation.Application.UserInfos.Edit
{
    internal sealed class EditUserInfoCommandHandler :
        ICommandHandler<EditUserInfoCommand, Guid>
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditUserInfoCommandHandler(IUserInfoRepository userInfoRepository, IUnitOfWork unitOfWork)
        {
            _userInfoRepository = userInfoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(EditUserInfoCommand request, CancellationToken cancellationToken)
        {

            var userInfo = await _userInfoRepository.FindEditAsync(request.UserInfoId, cancellationToken);

            if (userInfo is null)
            {
                return Result.Failure<Guid>(UserInfoErrors.NotFoundUserInfo);
            }


            userInfo.Edit(request.MaritalStatus,
                request.ChildCount,
                request.BirthOstandId,
                request.BirthShahrId,
                request.AddressOstanId,
                request.AddressShahrId,
                request.Address,
                request.PersonalPicGUID,
                request.KartMeliGUID,
                request.ShenasnamehGUID);


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userInfo.GUID;
        }
    }
}
