using Accreditation.Application.Abstractions.Messaging;
using SharedKernel;

namespace Accreditation.Application.CountryDivisions.Ostan.GetSelectList;

internal sealed class GetOstanSelectListQueryHandler
: IQueryHandler<GetOstanSelectListQuery, List<SelectListCountryDevisionResponse>>
{
    private readonly IOstanRepository _ostanRepository;

    public GetOstanSelectListQueryHandler(IOstanRepository ostanRepository)
    {
        _ostanRepository = ostanRepository;
    }
    public async Task<Result<List<SelectListCountryDevisionResponse>>> Handle(GetOstanSelectListQuery query,
                                                               CancellationToken cancellationToken)
    {
        return await _ostanRepository.GetSelectListByOstanIdAsync(cancellationToken);
    }
}