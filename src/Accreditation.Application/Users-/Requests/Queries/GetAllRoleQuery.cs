using Accrediation.Application.Services.UserServices.Dtos;
using MediatR;

namespace Accrediation.Application.Users.Requests.Queries
{
    public class GetAllRoleQuery : IRequest<List<GetAllRoleDto>>
    {

    }
}
