using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.UserInfos;
using SharedKernel;


namespace Accreditation.Application.UserInfos.GetById
{
    internal sealed class GetUserInfoByUserGuidQueryHandler :
        IQueryHandler<GetUserInfoByUserGuidQuery, GetUserInfoByUserGuidDto>
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public GetUserInfoByUserGuidQueryHandler(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        public async Task<Result<GetUserInfoByUserGuidDto>> Handle(GetUserInfoByUserGuidQuery request, CancellationToken cancellationToken)
        {
            var userInfo = await _userInfoRepository.FindAsync(request.UserGuid, cancellationToken);

            if (userInfo is null)
            {
                return Result.Failure<GetUserInfoByUserGuidDto>(UserInfoErrors.NotFoundUserInfo);
            }


            return await _userInfoRepository.GetUserInfoWithUserDetailsAsync(request.UserGuid, cancellationToken);



        }
    }
}
