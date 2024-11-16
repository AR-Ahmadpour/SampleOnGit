using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;
using System.Data;

namespace Accreditation.Application.OrgTypes.GetSelectList;

public sealed class GetOrgTypeSelectListQueryHandler
: IQueryHandler<GetOrgTypeSelectListQuery, List<GetOrgTypeSelectListResponse>>
{
    private readonly IDbConnectionFactory _factory;

    public GetOrgTypeSelectListQueryHandler(IDbConnectionFactory factory)
    {
        _factory = factory;
    }
    public async Task<Result<List<GetOrgTypeSelectListResponse>>> Handle(GetOrgTypeSelectListQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _factory.CreateConnection();
        const string sql = """
        SELECT
            GUID AS GUID,
            Title AS Title
        FROM OrgType
        """;

        var orgTypes = (await connection.
            QueryAsync<GetOrgTypeSelectListResponse>(sql)).ToList();

        return orgTypes;
    }
}
