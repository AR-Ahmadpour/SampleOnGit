using Accreditation.Api.Endpoints.EtebarDorehs.Adds;
using Accreditation.Api.Extensions;
using Accreditation.Api.Infrastructure;
using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Users.RegisterUser;
using Accreditation.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SharedKernel;

namespace Accreditation.Api.Endpoints.Users.Registers;

public class Register// : IEndpoint
{
    //public void MapEndpoint(IEndpointRouteBuilder app)
    //{
    //    app.MapPost("user/register", async (
    //    RegisterUserRequest request,
    //    ISender sender,
    //    CancellationToken cancellationToken) =>
    //    {
    //        var command = new RegisterUserCommand(
    //         request.Password,  request.FirstName, request.LastName, request.FatherName,
    //         request.BirthCertNo, request.BirthCertSerial, request.BirthPlace,
    //         request.Mobile, request.Tel, request.Email, request.GenderId,
    //         request.DeathDate, request.IsAlive, request.SabteAhvalApproved,
    //         request.IsDeleted, request.ImageId, request.NationalCode, request.BirthDate,
    //         request.AtbaKhareji,1,request.Password);

    //        Result<Guid> result = await sender.Send(command, cancellationToken);

    //        return result.Match(Results.Ok, CustomResults.Problem);
    //    }) 
    //      .AllowAnonymous()
    //      .WithTags(Tags.Users);
    //}
}
