using Accreditation.Application.Abstractions.Messaging;

namespace Accreditation.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<GetLoggedInUserDto>;
