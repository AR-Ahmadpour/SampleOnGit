using Accreditation.Api.Extensions;
using Accreditation.Api.Infrastructure;
using Accreditation.Application.EtebarDorehs.Add;
using MediatR;

namespace Accreditation.Api.Endpoints.EtebarDorehs.Adds;

public class Add : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("etebar-doreh", async (
        AddEtebarDorehRequest request,
        ISender sender,
        CancellationToken cancellationToken) =>
        {
            var command = new AddEtebarDorehCommand(request.OrgTypeGUID, request.Title, request.StartDate, request.EndDate, request.IsCurrent, request.SortOrder);

            var result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
     .RequireAuthorization()
     //.HasPermission(Permissions.UsersRead)
     .WithTags(Tags.EtebarDorehs);
    }
}
