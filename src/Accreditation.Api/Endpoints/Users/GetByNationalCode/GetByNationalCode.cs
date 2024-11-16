using Accreditation.Api.Extensions;
using Accreditation.Api.Infrastructure;
using Accreditation.Application.Mehvars.GetById;
using Accreditation.Application.Users.GetByNationalId;
using MediatR;

namespace Accreditation.Api.Endpoints.Users.GetByNationalCode;

public class GetByNationalCode : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
       // app.MapGet("user/{nationalCode}", async (
       //   string nationalCode,
       //   ISender sender,
       //   CancellationToken cancellationToken) =>
       // {
       //     var query = new GetUserByNationallCodeQuery(nationalCode);

       //     var result = await sender.Send(query, cancellationToken);

       //     return result.Match(Results.Ok, CustomResults.Problem);
       // })
       //.RequireAuthorization()
       ////.HasPermission(Permissions.UsersAccess);
       //// .MapToApiVersion(1);
       //.WithTags(Tags.Users);
    }
}
