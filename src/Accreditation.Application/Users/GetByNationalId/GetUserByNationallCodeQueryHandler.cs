using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Users.GetByNationalId;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Application.Users.GetById;

internal sealed class GetUserByNationallCodeQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserByNationallCodeQuery, GetUserResponse>
{
    public async Task<Result<GetUserResponse>> Handle(GetUserByNationallCodeQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByNationalCodeAsync(request.nationalCode, cancellationToken);

        if (user == null)
        {
            return Result.Failure<GetUserResponse>(UserErrors.InValidNationalCode);
        }
        else
        {
            return user;
        }
        //Change By Ahmadpour
        //return new GetUserResponse
        //{
        //    FirstName = user.FirstName,
        //    LastName  = user.LastName,
        //};
    }
}
