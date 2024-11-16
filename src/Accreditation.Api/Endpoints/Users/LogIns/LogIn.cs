using Accreditation.Api.Endpoints.Users.Registers;
using Accreditation.Api.Extensions;
using Accreditation.Api.Infrastructure;
using Accreditation.Application.Users.LogInUser;
using MediatR;
using SharedKernel;

namespace Accreditation.Api.Endpoints.Users.LogIns;

public class LogIn //: IEndpoint
{
    //public void MapEndpoint(IEndpointRouteBuilder app)
    //{
    //    app.MapPost("user/login", async (
    //    LogInUserRequest request,
    //    ISender sender,
    //    CancellationToken cancellationToken) =>
    //    {
    //        var command = new LogInUserCommand(request.NationalCode, request.Password);

    //        var result = await sender.Send(command, cancellationToken);

    //        return result.Match(Results.Ok, CustomResults.Problem);
    //    })
    //      .AllowAnonymous()
    //      .WithTags(Tags.Users);
    //}
}
