using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Domain.CountryDivisions.Shahrestan.Abstractions;
using SharedKernel;

namespace Accreditation.Application.CountryDivisions.Shahrestan.GetSelectList;

internal sealed class GetShahrestanSelectListQueryHandler
: IQueryHandler<GetShahrestanSelectListQuery, List<SelectListCountryDevisionResponse>>
{
    private readonly IShahrestanRepository _shahrestanRepository;

    public GetShahrestanSelectListQueryHandler(IShahrestanRepository shahrestanRepository)
    {
        _shahrestanRepository = shahrestanRepository;
    }
    public async Task<Result<List<SelectListCountryDevisionResponse>>> Handle(GetShahrestanSelectListQuery query,
                                                               CancellationToken cancellationToken)
    {
        return await _shahrestanRepository.GetSelectListSharestanAsync(query.ostanId, cancellationToken);
    }
}