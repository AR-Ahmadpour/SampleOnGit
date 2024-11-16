using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.Users;
using SharedKernel;

namespace Accreditation.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler
    (
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider,
        IAuthenticationService authenticationService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand, Guid>
{ 
    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var createdBy = new Guid();

        var user = User.Create(
         request.FirstName, request.LastName, request.FatherName,
         request.BirthCertNo, request.BirthCertSerial, request.BirthPlace,
         request.Mobile, request.Tel, request.Email, request.GenderId,
         request.  DeathDate, request.IsAlive, request.SabteAhvalApproved,
         request.IsDeleted, request. ImageId, request.NationalCode, request. BirthDate,
         request.AtbaKhareji, dateTimeProvider.Now, createdBy,request.RoleId,request.Password);

        string identityId = await authenticationService.RegisterAsync(
            user,
            request.Password,
            cancellationToken);

        user.SetSSOUserId(new Guid(identityId));

        userRepository.Add(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.GUID;
    }
}
