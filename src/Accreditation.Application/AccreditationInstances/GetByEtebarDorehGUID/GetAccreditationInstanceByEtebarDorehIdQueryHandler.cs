using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.AccreditationInstances.GetByEtebarDorehGUID
{
    internal class GetAccreditationInstanceByEtebarDorehIdQueryHandler
    :IQueryHandler<GetAccreditationInstanceByEtebarDorehIdQuery , List<GetAccreditationInstanceByEtebarDorehIdQueryDto> >
    {
        private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;

        public GetAccreditationInstanceByEtebarDorehIdQueryHandler
            (IAccreditationInstanceRepository accreditationInstanceRepository )
        {
            _accreditationInstanceRepository = accreditationInstanceRepository;
        }

        public async Task<Result<List<GetAccreditationInstanceByEtebarDorehIdQueryDto> >> Handle(GetAccreditationInstanceByEtebarDorehIdQuery request, CancellationToken cancellationToken)
        {
            return await _accreditationInstanceRepository.GetAccreditationInstanceByEtebarDorehId(request, cancellationToken);
        }
    }
}
