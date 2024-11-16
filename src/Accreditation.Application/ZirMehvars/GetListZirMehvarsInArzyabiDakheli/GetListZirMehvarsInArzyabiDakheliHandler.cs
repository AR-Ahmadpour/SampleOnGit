using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using Accreditation.Application.Mehvars.GetListMehvarsInArzyabiDakheli;
using Accreditation.Application.Mehvars;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.ZirMehvars.GetListZirMehvarsInArzyabiDakheli
{
    internal class GetListZirMehvarsInArzyabiDakheliHandler
        :IQueryHandler<GetListZirMehvarsInArzyabiDakheliQuery,List<GetListZirMehvarsInArzyabiDakheliDto>>
    {
        private readonly IZirMehvarRepository _zirmehvarRepository;
        private readonly IMehvarRepository _mehvarRepository;

        public GetListZirMehvarsInArzyabiDakheliHandler(IZirMehvarRepository zirmehvarRepository, IMehvarRepository mehvarRepository)
        {
            _zirmehvarRepository = zirmehvarRepository;
            _mehvarRepository = mehvarRepository;
        }

        public async Task<Result<List<GetListZirMehvarsInArzyabiDakheliDto>>> Handle(GetListZirMehvarsInArzyabiDakheliQuery request, CancellationToken cancellationToken)
        {
            //var mehvar = await _mehvarRepository.GetByIdAsync(request.ZirMehvarsId, cancellationToken);

            //if (mehvar == null)
            //{
            //    return Result.Failure<List<GetListZirMehvarsInArzyabiDakheliDto>>(MehvarErrors.NotFound);
            //}

            return await _zirmehvarRepository.GetListZirMehvarsInArzyabiDakheliAsync(request, cancellationToken);
        }
    }
}
