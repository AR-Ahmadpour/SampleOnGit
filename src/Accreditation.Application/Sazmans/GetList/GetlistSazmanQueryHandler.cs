using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Sazmans;
using SharedKernel;

namespace Accreditation.Application.Sazmans.GetList
{
    internal sealed class GetlistSazmanQueryHandler :
        IQueryHandler<GetListSazmanQuery, List<GetListSazmanDto>>
    {
        private readonly ISazmanRepository _sazmanRepository;

        public GetlistSazmanQueryHandler(ISazmanRepository sazmanRepository)
        {
            _sazmanRepository = sazmanRepository;
        }

        public async Task<Result<List<GetListSazmanDto>>> Handle(GetListSazmanQuery request, CancellationToken cancellationToken)
        {
            return await _sazmanRepository.GetListAsync(cancellationToken);
        }
    }
}
