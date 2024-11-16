using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Dapper;
using MediatR;
using SharedKernel;
using System.Data;

namespace Accreditation.Application.EtebarDorehs.GetById;

public sealed class GetEtebarDorehByIdQueryHandler
: IQueryHandler<GetEtebarDorehByIdQuery, GetEtebarDorehResponse>
{
    //private readonly IDbConnectionFactory _factory;
    private readonly IEtebarDorehRepository _etebarDorehRepository;

    public GetEtebarDorehByIdQueryHandler(/*IDbConnectionFactory factory,*/ IEtebarDorehRepository etebarDorehRepository)
    {
        _etebarDorehRepository = etebarDorehRepository;
        //_factory = factory;
    }
    public async Task<Result<GetEtebarDorehResponse>> Handle(GetEtebarDorehByIdQuery query, CancellationToken cancellationToken)
    {
        //using IDbConnection connection = _factory.CreateConnection();
        //const string sql = """
        //SELECT
        //    GUID AS GUID,
        //    OrgTypeGUID AS   OrgTypeGUID,
        //    Title AS Title,
        //    StartDate AS StartDate,
        //    EndDate AS EndDate,
        //    SortOrder AS SortOrder,
        //    IsCurrent AS IsCurrent,
        //    IsDeleted As IsDeleted
        //FROM EtebarDoreh
        //WHERE GUID = @GUID 
        //""";

        //GetEtebarDorehResponse? etebarDoreh = await connection.
        //    QueryFirstOrDefaultAsync<GetEtebarDorehResponse>(sql, query);

        //if (etebarDoreh is null)
        //{
        //    return Result.Failure<GetEtebarDorehResponse>(EtebarDorehErrors.NotFound);
        //}

        //return etebarDoreh;


        var etebarDoreh = await _etebarDorehRepository.GetByIdAsync(query.GUID, cancellationToken);


        if (etebarDoreh is null)
        {
            return Result.Failure<GetEtebarDorehResponse>(EtebarDorehErrors.NotFound);
        }

        var response = new GetEtebarDorehResponse
        {
            Title = etebarDoreh.Title,
            GUID = etebarDoreh.GUID,
            StartDate = etebarDoreh.StartDate,
            EndDate = etebarDoreh.EndDate,
            IsCurrent = etebarDoreh.IsCurrent,
            OrgTypeGUID = etebarDoreh.OrgTypeGUID,
            IsDeleted = etebarDoreh.IsDeleted,
            SortOrder = etebarDoreh.SortOrder

        };


        return response;
    }
}

