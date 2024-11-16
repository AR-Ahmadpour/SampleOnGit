using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Users.GetById;


namespace Accreditation.Application.Users.GetByNationalId;

public sealed record GetUserByNationallCodeQuery(string nationalCode) : IQuery<GetUserResponse>;

