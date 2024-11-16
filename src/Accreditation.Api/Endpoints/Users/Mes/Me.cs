using Accreditation.Api.Extensions;
using Accreditation.Api.Infrastructure;
using Accreditation.Application.Users.GetLoggedInUser;
using Accreditation.Infrastructure.Authorization;
using MediatR;
using SharedKernel;

namespace Accreditation.Api.Endpoints.Users.Mes;

public class Me : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/me", [HasPermission(Permissions.UsersRead)] async (
        ISender sender,
        CancellationToken cancellationToken) =>
        {
            var query = new GetLoggedInUserQuery();

            Result<GetLoggedInUserDto> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
          .WithTags(Tags.Users);
    }
}
