using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.AccreditationInstances;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Application.Common.Interfaces.Persistence.Headers;
using Accreditation.Application.Fields;
using Accreditation.Application.Headers.GetList;
using SharedKernel;

namespace Accreditation.Application.Headers.GetBy
{
    internal sealed class GetHeaderQueryHandler :
        IQueryHandler<GetHeaderQuery, GetHeaderDto>
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IAccreditationInstanceRepository _accreditationInstanceRepository;
        private readonly IHeaderRepository _headerRepository;

        public GetHeaderQueryHandler(IFieldRepository fieldRepository, IAccreditationInstanceRepository accreditationInstanceRepository, IHeaderRepository headerRepository)
        {
            _fieldRepository = fieldRepository;
            _accreditationInstanceRepository = accreditationInstanceRepository;
            _headerRepository = headerRepository;
        }


        public async Task<Result<GetHeaderDto>> Handle(GetHeaderQuery request, CancellationToken cancellationToken)
        {
            if (await _fieldRepository.FindAsync(request.FieldGuid,cancellationToken) is null)
            {
                return Result.Failure<GetHeaderDto>(FieldsErrors.NotFound);
            }

            if (!await _accreditationInstanceRepository.Any(request.AccreditationInstanceGuid,cancellationToken))
            {
                return Result.Failure<GetHeaderDto>(AccreditationInstanceErrors.NotFound);
            }

            return await _headerRepository.GetHeader(request.FieldGuid,request.AccreditationInstanceGuid,cancellationToken);
        }
    }
}
