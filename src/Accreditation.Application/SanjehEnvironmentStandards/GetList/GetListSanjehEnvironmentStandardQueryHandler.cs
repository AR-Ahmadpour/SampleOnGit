using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.SanjehEnvironmentStandards;
using SharedKernel;

namespace Accreditation.Application.SanjehEnvironmentStandards.GetList;

internal sealed class GetListSanjehEnvironmentStandardQueryHandler(

ISanjehEnvironmentStandardRepository sanjehEnvironmentStandardRepository)
: IQueryHandler<GetListSanjehEnvironmentStandardQuery, List<GetListSanjehEnvironmentStandardDto>>
{
    public async Task<Result<List<GetListSanjehEnvironmentStandardDto>>> Handle(GetListSanjehEnvironmentStandardQuery request, CancellationToken cancellationToken)
    {
        var getSanjehAllEnvironmentStandardLevel = await sanjehEnvironmentStandardRepository.GetAllSanjehEnvironmentStandard(cancellationToken);

        //var getAllSanjehEnvironmentStandardResponseList = getSanjehAllEnvironmentStandardLevel
        //    .Select(x => new GetListSanjehEnvironmentStandardResponse
        //    {
        //        Guid = x.Guid,
                
        //    }).ToList();

        return Result.Success(getSanjehAllEnvironmentStandardLevel);
    }
}
