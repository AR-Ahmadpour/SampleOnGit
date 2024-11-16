using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.MaghtaTahsilis;
using Accreditation.Application.Common.Interfaces.Persistence.ReshtehMaghta;
using Accreditation.Application.MaghtaTahsilis;
using SharedKernel;

namespace Accreditation.Application.ReshtehMaghtas.GetList
{
    internal sealed class GetListReshtehMaghtaTahsiliQueryHandler :
        IQueryHandler<GetListReshtehMaghtaTahsiliQuery, List<GetListReshtehMaghtaQueryDto>>
    {
        private readonly IMaghtaTahsiliRepository _maghtaTahsiliRepository;
        private readonly IReshtehMaghtaRepository _reshtehMaghtaRepository;

        public GetListReshtehMaghtaTahsiliQueryHandler(IMaghtaTahsiliRepository maghtaTahsiliRepository, IReshtehMaghtaRepository reshtehMaghtaRepository)
        {
            _maghtaTahsiliRepository = maghtaTahsiliRepository;
            _reshtehMaghtaRepository = reshtehMaghtaRepository;
        }

        public async Task<Result<List<GetListReshtehMaghtaQueryDto>>> Handle(GetListReshtehMaghtaTahsiliQuery request, CancellationToken cancellationToken)
        {
            if (!await _maghtaTahsiliRepository.FindAsync(request.MaghtaGuid, cancellationToken))
            {
                return Result.Failure<List<GetListReshtehMaghtaQueryDto>>(MaghtaTahsiliErrors.NotFoundMaghta);
            }

            return await _reshtehMaghtaRepository.GetReshtehsByMaghtaIdAsync(request.MaghtaGuid, cancellationToken);
        }
    }
}
