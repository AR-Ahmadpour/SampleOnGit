using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.CountryDivisions.BakhshLocation.Abstractions;
using SharedKernel;

namespace Accreditation.Application.CountryDivisions.BakhshLocation.GetSelectList;

internal sealed class GetBakhshLocationSelectListQueryHandler
: IQueryHandler<GetBakhshLocationSelectListQuery, List<SelectListCountryDevisionResponse>>
{
    private readonly IBakhshLocationRepository _bakhshLocationRepository;

    public GetBakhshLocationSelectListQueryHandler(IBakhshLocationRepository bakhshLocationRepository)
    {
        _bakhshLocationRepository = bakhshLocationRepository;
    }
    public async Task<Result<List<SelectListCountryDevisionResponse>>> Handle(GetBakhshLocationSelectListQuery query,
                                                               CancellationToken cancellationToken)
    {
        return await _bakhshLocationRepository.GetSelectListBakhshLocationAsync(query.shahrestanId, cancellationToken);
    }
}