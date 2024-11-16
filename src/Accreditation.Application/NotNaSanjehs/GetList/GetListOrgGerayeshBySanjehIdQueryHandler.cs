using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.NotNaSanjehs;
using Accreditation.Application.Common.Interfaces.Persistence.Organization;
using Accreditation.Application.Common.Interfaces.Persistence.Sanjehs;
using Accreditation.Application.Sanjehs;
using SharedKernel;

namespace Accreditation.Application.NotNaSanjehs.GetList
{
    internal sealed class GetListOrgGerayeshBySanjehIdQueryHandler :
        IQueryHandler<GetListOrgGerayeshBySanjehIdQuery, List<GetListOrgGerayeshBySanjehIdDto>>

    {
        private readonly ISanjehRepository _sanjehRepository;
        private readonly INotNaSanjehRepository _notNaSanjehRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public GetListOrgGerayeshBySanjehIdQueryHandler(ISanjehRepository sanjehRepository, INotNaSanjehRepository notNaSanjehRepository, IOrganizationRepository organizationRepository)
        {
            _sanjehRepository = sanjehRepository;
            _notNaSanjehRepository = notNaSanjehRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<Result<List<GetListOrgGerayeshBySanjehIdDto>>> Handle(GetListOrgGerayeshBySanjehIdQuery request, CancellationToken cancellationToken)
        {
            if (await _sanjehRepository.FindAsync(request.SanjehGuid, cancellationToken) is null)
            {
                return Result.Failure<List<GetListOrgGerayeshBySanjehIdDto>>(SanjehErrors.NotFoundSanjeh);
            }


          

            return await _notNaSanjehRepository.GetAllOrgGerayeshBySanjehId(request.SanjehGuid, cancellationToken);
        }
    }
}
