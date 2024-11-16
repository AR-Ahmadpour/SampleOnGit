using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.CountryDivisions.BakhshLocation;
using Accreditation.Domain.CountryDivisions.BakhshLocation.Abstractions;
using Accreditation.Domain.CountryDivisions.Shahr.Abstractions;
using Accreditation.Domain.CountryDivisions.Shahrestan.Abstractions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.CountryDivisions.Shahr.GetSelectList
{
    internal sealed class GetShahrByBakhshSelectListQueryHandler :
        IQueryHandler<GetShahrByBakhshSelectListQuery, List<SelectListCountryDevisionResponse>>
    {

        private readonly IBakhshLocationRepository _bakhshLocationRepository;
        private readonly IShahrRepository _ShahrRepository;

        public GetShahrByBakhshSelectListQueryHandler(IBakhshLocationRepository bakhshLocationRepository, IShahrRepository shahrRepository)
        {
            _bakhshLocationRepository = bakhshLocationRepository;
            _ShahrRepository = shahrRepository;
        }

        public async Task<Result<List<SelectListCountryDevisionResponse>>> Handle(GetShahrByBakhshSelectListQuery request, CancellationToken cancellationToken)
        {
            if (await _bakhshLocationRepository.FindAsync(request.BakhshId, cancellationToken) is null)
            {
                return Result.Failure<List<SelectListCountryDevisionResponse>>(BakhshErrors.NotFoundBakhsh);
            }


            return await _ShahrRepository.GetSelectListSharByBakhshAsync(request.BakhshId, cancellationToken);
        }
    }
}
