using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.MaghtaTahsilis;
using SharedKernel;

namespace Accreditation.Application.MaghtaTahsilis.GetList
{
    internal sealed class GetAllMaghtaTahsiliQueryHandler :
        IQueryHandler<GetListMaghtaTahsiliQuery, List<GetAllMaghtaTahsiliQueryDto>>
    {
        private readonly IMaghtaTahsiliRepository _maghtaTahsiliRepository;

        public GetAllMaghtaTahsiliQueryHandler(IMaghtaTahsiliRepository maghtaTahsiliRepository)
        {
            _maghtaTahsiliRepository = maghtaTahsiliRepository;
        }

        public async Task<Result<List<GetAllMaghtaTahsiliQueryDto>>> Handle(GetListMaghtaTahsiliQuery request, CancellationToken cancellationToken)
        {
            return await _maghtaTahsiliRepository.GetListMaghtaTahsiliAsync(cancellationToken);
        }
    }
}
