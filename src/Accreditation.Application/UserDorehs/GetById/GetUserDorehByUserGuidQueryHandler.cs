using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.UserDorehs;
using SharedKernel;

namespace Accreditation.Application.UserDorehs.GetById
{
    internal sealed class GetUserDorehByUserGuidQueryHandler
        : IQueryHandler<GetUserDorehByUserGuidQuery, List<GetUserDorehByUserGuidDto>>
    {
        private readonly IUserDorehRepository _userDorehRepository;

        public GetUserDorehByUserGuidQueryHandler(IUserDorehRepository userDorehRepository)
        {
            _userDorehRepository = userDorehRepository;
        }

        public async Task<Result<List<GetUserDorehByUserGuidDto>>> Handle(GetUserDorehByUserGuidQuery request, CancellationToken cancellationToken)
        {
            var userDoreh =await _userDorehRepository.FindAsync(request.UserGuid, cancellationToken);

            if (userDoreh is null)
            {
                return Result.Failure<List<GetUserDorehByUserGuidDto>>(UserDorehErrors.UserFound);
            }

            return await _userDorehRepository.GetUserDorehInInfoWithUserDetailsAsync(request.UserGuid,cancellationToken);
        }
    }
}
