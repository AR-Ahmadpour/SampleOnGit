using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.SanjehEnvironmentStandards;
using Accreditation.Application.Common.Interfaces.Persistence.Sanjehs;
using Accreditation.Application.Sanjehs;
using SharedKernel;

namespace Accreditation.Application.SanjehEnvironmentStandards.GetList
{
    internal class GetListSanjehEnvironmentStandardBySanjehIdQueryHandler :
        IQueryHandler<GetListSanjehEnvironmentStandardBySanjehIdQuery, List<GetListSanjehEnvironmentStandardBySanjehIdDto>>
    {

        private readonly ISanjehRepository _sanjehRepository;
        private readonly ISanjehEnvironmentStandardRepository _sanjehEnvironmentStandardRepository;

        public GetListSanjehEnvironmentStandardBySanjehIdQueryHandler(ISanjehRepository sanjehRepository, ISanjehEnvironmentStandardRepository sanjehEnvironmentStandardRepository)
        {
            _sanjehRepository = sanjehRepository;
            _sanjehEnvironmentStandardRepository = sanjehEnvironmentStandardRepository;
        }

        public async Task<Result<List<GetListSanjehEnvironmentStandardBySanjehIdDto>>> Handle(GetListSanjehEnvironmentStandardBySanjehIdQuery request, CancellationToken cancellationToken)
        {
            if (await _sanjehRepository.FindAsync(request.SanjehGuid, cancellationToken) is null)
            {
                return Result.Failure<List<GetListSanjehEnvironmentStandardBySanjehIdDto>>(SanjehErrors.NotFoundSanjeh);
            }

            return await _sanjehEnvironmentStandardRepository.GetAllSanjehEnvironmentStandardBySanjehId(request.SanjehGuid, cancellationToken);

        }
    }
}
