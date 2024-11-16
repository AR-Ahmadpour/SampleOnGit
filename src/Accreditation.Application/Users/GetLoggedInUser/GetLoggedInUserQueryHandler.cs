using System.Data;
using Accreditation.Application.Abstractions.Authentication;
using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging; 
using Dapper;
using SharedKernel;

namespace Accreditation.Application.Users.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler
    : IQueryHandler<GetLoggedInUserQuery, GetLoggedInUserDto>
{
    private readonly IDbConnectionFactory _sqlConnectionFactory;
    private readonly IUserContext _userContext;

    public GetLoggedInUserQueryHandler(IDbConnectionFactory factory,
        IUserContext userContext)
    {
        _sqlConnectionFactory = factory;
        _userContext = userContext;
    }

    public async Task<Result<GetLoggedInUserDto>> Handle(
        GetLoggedInUserQuery request,
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id AS Id,
                first_name AS FirstName,
                last_name AS LastName,
                email AS Email
            FROM users
            WHERE identity_id = @IdentityId
            """;

        GetLoggedInUserDto user = await connection.QuerySingleAsync<GetLoggedInUserDto>(
            sql,
            new
            {
                _userContext.UserGUId
            });

        return user;
    }
}
