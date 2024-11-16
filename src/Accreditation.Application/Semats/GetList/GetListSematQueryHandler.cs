using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Semats;
using SharedKernel;

namespace Accreditation.Application.Semats.GetList
{
    internal sealed class GetListSematQueryHandler :
        IQueryHandler<GetListSematQuery, List<GetListSematDto>>
    {
        private readonly ISematRepository _sematRepository;

        public GetListSematQueryHandler(ISematRepository sematRepository)
        {
            _sematRepository = sematRepository;
        }

        public async Task<Result<List<GetListSematDto>>> Handle(GetListSematQuery request, CancellationToken cancellationToken)
        {
            return await _sematRepository.GetListAsync(cancellationToken);
        }
    }
}
