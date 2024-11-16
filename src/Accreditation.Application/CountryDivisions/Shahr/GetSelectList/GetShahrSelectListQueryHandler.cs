using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.CountryDivisions.Ostan;
using Accreditation.Domain.CountryDivisions.Shahr.Abstractions;
using SharedKernel;

namespace Accreditation.Application.CountryDivisions.Shahrestan.GetSelectList;

internal sealed class GetShahrSelectListQueryHandler
: IQueryHandler<GetShahrSelectListQuery, List<SelectListCountryDevisionResponse>>
{
    private readonly IShahrRepository _shahrRepository;
    private readonly IOstanRepository _ostanRepository;
    

    public GetShahrSelectListQueryHandler(IShahrRepository shahrRepository, IOstanRepository ostanRepository)
    {
        _shahrRepository = shahrRepository;
        _ostanRepository = ostanRepository;
    }

    public async Task<Result<List<SelectListCountryDevisionResponse>>> Handle(GetShahrSelectListQuery query,
                                                               CancellationToken cancellationToken)
    {
        if (await _ostanRepository.FindAsync(query.ostanId,cancellationToken) is null)
        {
            return Result.Failure<List<SelectListCountryDevisionResponse>>(OstanErrors.NotFoundOstan);
        }

        return await _shahrRepository.GetSelectListSharAsync(query.ostanId, cancellationToken);
    }
}