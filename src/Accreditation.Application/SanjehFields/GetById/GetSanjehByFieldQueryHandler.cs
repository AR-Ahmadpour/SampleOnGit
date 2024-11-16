using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Application.Common.Interfaces.Persistence.SanjehFields;
using Accreditation.Application.Sanjehs;
using SharedKernel;

namespace Accreditation.Application.SanjehFields.GetById
{
    internal sealed class GetSanjehByFieldQueryHandler :
        IQueryHandler<GetSanjehByFieldQuery, List<GetSanjehsByFieldDto>>
    {

        private readonly IFieldRepository _fieldRepository;
        private readonly ISanjehFieldRepository _sanjehFieldRepository;

        public GetSanjehByFieldQueryHandler(IFieldRepository fieldRepository,
            ISanjehFieldRepository sanjehFieldRepository)
        {
            _fieldRepository = fieldRepository;
            _sanjehFieldRepository = sanjehFieldRepository;
        }

        public async Task<Result<List<GetSanjehsByFieldDto>>> Handle(GetSanjehByFieldQuery request, CancellationToken cancellationToken)
        {
            if (await _fieldRepository.FindAsync(request.fieldId, cancellationToken) is null)
            {
                return Result.Failure<List<GetSanjehsByFieldDto>>(SanjehErrors.NotFoundSanjeh);
            }

            return await _sanjehFieldRepository.GetAllSanjehsByFieldId(request.fieldId, request.mehvarId,cancellationToken);
        }
    }
}
