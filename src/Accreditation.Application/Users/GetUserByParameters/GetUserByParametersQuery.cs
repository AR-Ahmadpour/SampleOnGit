using Accreditation.Application.Abstractions.Messaging;
using System;

namespace Accreditation.Application.Users.GetByParameters
{
    public sealed record GetUserByParametersQuery(string SearchParam):
         //string? FirstName , string? LastName ,string? NationalCode) :
         IQuery<List<GetUserByParametersDto>>;        

}
