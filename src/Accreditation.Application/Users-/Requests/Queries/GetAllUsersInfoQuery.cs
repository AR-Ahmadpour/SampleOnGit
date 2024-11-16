using Accrediation.Application.Services.UserServices.Dtos;
using MediatR;
using Accrediation.Application.Common.Models;

namespace Accrediation.Application.Users.Requests.Queries
{
    public class GetAllUsersInfoQuery : IRequest<PagedList<GetAllUsersInfoDto>>
    {
        public PagingParams PagingParams { get; set; }
    }
}
