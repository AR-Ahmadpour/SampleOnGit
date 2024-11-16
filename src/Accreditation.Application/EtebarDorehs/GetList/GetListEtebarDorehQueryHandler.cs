using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Models;
using Accreditation.Domain.EtebarDorehs.Dtos;
using SharedKernel;

namespace Accreditation.Application.EtebarDorehs.GetList;

public sealed class GetListEtebarDorehQueryHandler
: IQueryHandler<GetListEtebarDorehQuery, PagedList<GetListDto>>
{
    private readonly IEtebarDorehRepository _etebarDorehRepository;

    public GetListEtebarDorehQueryHandler(IEtebarDorehRepository etebarDorehRepository)
    {
        _etebarDorehRepository = etebarDorehRepository;
    }

    public async Task<Result<PagedList<GetListDto>>> Handle
        (GetListEtebarDorehQuery request, CancellationToken cancellationToken)
    {
        return
            await _etebarDorehRepository.GetListAsync(
            request.PageNumber,
            request.PageSize,
            request.Orgtype,
            cancellationToken);

    }
}

#region Sample Of Dapper
//please remove these at the end of the project:
//using IDbConnection connection = factory.CreateConnection();

//        var startRow = (query.PageNumber - 1) * query.PageSize;
//var endRow = query.PageNumber * query.PageSize;

//var sql = @"SELECT 
//                etebarDoreh.GUID,
//                etebarDoreh.Title,
//                etebarDoreh.CreationDate,
//                etebarDoreh.UpdateDate,
//                etebarDoreh.CreatedByGUID,
//                etebarDoreh.UpdatedByGUID,
//                etebarDoreh.StartDate,
//                etebarDoreh.EndDate,
//                orgType.Title OrgTypeTitle
//                      FROM EtebarDoreh etebarDoreh
//                      INNER JOIN OrgType orgType ON etebarDoreh.OrgTypeGUID = orgType.GUID
//                      ORDER BY etebarDoreh.SORTORDER  
//                      OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
//                SELECT COUNT(*) FROM EtebarDoreh  WHERE  etebarDoreh.IsDeleted = @IsDeleted";

//var parameters = new
//{
//    Offset = (query.PageNumber - 1) * query.PageSize,
//    PageSize = query.PageSize
//};

//IPaginator<GetListEtebarDorehResponse> result = null;

//        using (var data = await connection.QueryMultipleAsync(sql, parameters))
//        {
//            var list = (await data.ReadAsync<GetListEtebarDorehResponse>()).AsList();
//var totalCount = await data.ReadSingleAsync<int>();
//result = new PagedList<GetListEtebarDorehResponse>(
//totalCount,
//list,
//query.PageNumber,
//query.PageSize);


//        };
//return result;
#endregion