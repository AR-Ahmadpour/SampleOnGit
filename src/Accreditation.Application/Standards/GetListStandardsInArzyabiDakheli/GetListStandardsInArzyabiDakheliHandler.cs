using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Standards.GetListStandardsInArzyabiDakheli
{
    internal class GetListStandardsInArzyabiDakheliHandler
        :IQueryHandler<GetListStandardsInArzyabiDakheliQuery,List<GetListStandardsInArzyabiDakheliDto>>
    {
        private readonly IStandardRepository _standardRepository;

        public GetListStandardsInArzyabiDakheliHandler(IStandardRepository standardRepository)
        {
            _standardRepository = standardRepository;
        }

        public async Task<Result<List<GetListStandardsInArzyabiDakheliDto>>> Handle(GetListStandardsInArzyabiDakheliQuery request, CancellationToken cancellationToken)
        {
            return await _standardRepository.GetListZirMehvarsInArzyabiDakheliAsync(request, cancellationToken);
        }
    }
}
