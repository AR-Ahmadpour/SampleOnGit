using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards;
using SharedKernel;

namespace Accreditation.Application.EnvironmentStandards.GetSelectList;

internal sealed class GetEnvironmentStandardSelectListQueryHandler
: IQueryHandler<GetEnvironmentStandardSelectListQuery, List<SelectListResponse>>
{
    private readonly IEnvironmentStandardRepository _environmentStandardRepository;

    public GetEnvironmentStandardSelectListQueryHandler(IEnvironmentStandardRepository environmentStandardRepository)
    {
        _environmentStandardRepository = environmentStandardRepository;
    }

    public async Task<Result<List<SelectListResponse>>> Handle(GetEnvironmentStandardSelectListQuery request, CancellationToken cancellationToken)
    {
        var environmentStandardLevelList = await _environmentStandardRepository
            .GetSelectList(cancellationToken);

        return Result.Success(environmentStandardLevelList);
    }
}
