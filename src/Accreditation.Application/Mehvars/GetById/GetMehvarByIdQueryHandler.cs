using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using SharedKernel;

namespace Accreditation.Application.Mehvars.GetById;

internal sealed class GetMehvarByIdQueryHandler
    : IQueryHandler<GetMehvarByIdQuery, GetMehvarResponse>
{
    private readonly IMehvarRepository _mehvarRepository;

    public GetMehvarByIdQueryHandler(IMehvarRepository mehvarRepository)
    {
        _mehvarRepository = mehvarRepository;
    }
    public async Task<Result<GetMehvarResponse>> Handle(GetMehvarByIdQuery request, CancellationToken cancellationToken)
    {
        var mehvar = await _mehvarRepository.GetByIdAsync(request.GUID, cancellationToken);

        if (mehvar == null)
        {
            return Result.Failure<GetMehvarResponse>(MehvarErrors.NotFound);
        }

        return new GetMehvarResponse
        {
            Title = mehvar.Title,
            SortOrder = mehvar.SortOrder,
            WeightedCoefficient = mehvar.WeightedCoefficient,
            EtebarDoreTitle = mehvar.EtebarDoreh?.Title,
            OrganizationTitle = mehvar.EtebarDoreh?.OrgType?.Title
        };
    }
}
