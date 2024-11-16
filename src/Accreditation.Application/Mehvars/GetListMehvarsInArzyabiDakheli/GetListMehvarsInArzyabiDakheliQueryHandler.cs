using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Mehvars.GetById;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Mehvars.GetListMehvarsInArzyabiDakheli
{
    internal class GetListMehvarsInArzyabiDakheliQueryHandler
        :IQueryHandler<GetListMehvarsInArzyabiDakheliQuery, List<GetListMehvarsInArzyabiDakheliQueryDto>>
    {
        private readonly IMehvarRepository _mehvarRepository;

        public GetListMehvarsInArzyabiDakheliQueryHandler(IMehvarRepository mehvarRepository)
        {
            _mehvarRepository = mehvarRepository;
        }

        public async Task<Result<List<GetListMehvarsInArzyabiDakheliQueryDto>>> Handle(GetListMehvarsInArzyabiDakheliQuery request, CancellationToken cancellationToken)
        {
            //var mehvar = await _mehvarRepository.GetByIdAsync(request.EtebardorehId, cancellationToken);

            //if (mehvar == null)
            //{
            //    return Result.Failure< List<GetListMehvarsInArzyabiDakheliQueryDto>>(MehvarErrors.NotFound);
            //}

            return await _mehvarRepository.GetListMehvarsInArzyabiDakheliAsync(request, cancellationToken);
        }
    }
}
