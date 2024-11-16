using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.NoeKhedmats;
using SharedKernel;

namespace Accreditation.Application.NoeKhedmats.GetList
{
    internal sealed class GetListNoeKhedmatQueryHandler :
        IQueryHandler<GetListNoeKhedmatQuery, List<GetListNoeKhedmatDto>>
    {
        private readonly INoeKhedmatRepository _noeKhedmatRepository;

        public GetListNoeKhedmatQueryHandler(INoeKhedmatRepository noeKhedmatRepository)
        {
            _noeKhedmatRepository = noeKhedmatRepository;
        }

        public async Task<Result<List<GetListNoeKhedmatDto>>> Handle(GetListNoeKhedmatQuery request, CancellationToken cancellationToken)
        {
            return await _noeKhedmatRepository.GetListAsync(cancellationToken);
        }
    }
}
