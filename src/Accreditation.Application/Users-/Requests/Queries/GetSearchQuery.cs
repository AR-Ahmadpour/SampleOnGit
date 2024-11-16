using Accrediation.Application.Common.Models;
using Accrediation.Application.Services.UserServices.Dtos;
using MediatR;

namespace Accrediation.Application.Users.Requests.Queries
{
    public class GetSearchQuery : IRequest<PagedList<GetAllUsersInfoDto>>
    {
        public GetSearchQuery() { }

        public PagingParams PagingParams { get; set; } = new();

        public string? FirstName { get; set; }


        public string? LastName { get; set; }

        public string? FatherName { get; set; }

        public string? Email { get; set; }


        public string? Username { get; set; }

        public string? NationalCode { get; set; }


        public string? PhoneNumber { get; set; }


    }
}
