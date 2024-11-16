using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Tahsilats;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Tahsilats.GetById
{
    internal sealed class GetTahsilatByUserGuidQueryHandler :
        IQueryHandler<GetTahsilatByUserGuidQuery, GetTahsilatByUserGuidDto>
    {
        private readonly ITahsilatRepository _tahsilatRepository;

        public GetTahsilatByUserGuidQueryHandler(ITahsilatRepository tahsilatRepository)
        {
            _tahsilatRepository = tahsilatRepository;
        }

        public async Task<Result<GetTahsilatByUserGuidDto>> Handle(GetTahsilatByUserGuidQuery request, CancellationToken cancellationToken)
        {
            var tahsilat = await _tahsilatRepository.FindAsync(request.UserGuid, cancellationToken);

            if (tahsilat == null)
            {
                return Result.Failure<GetTahsilatByUserGuidDto>(TahsilatError.TahsilatNotFound);
            }

            return await _tahsilatRepository.GetTahsilatInfoWithUserDetailsAsync(request.UserGuid,cancellationToken);
        }
    }
}
