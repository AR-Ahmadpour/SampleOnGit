using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.Common.Interfaces.Persistence.Organization;
using Accreditation.Application.Common.Interfaces.Persistence.Sanjehs;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using Accreditation.Application.Common.Interfaces.Persistence.ZirMehvars;
using SharedKernel;

namespace Accreditation.Application.BreadCrumbs.Get
{
    internal sealed class GetBreadCrumbQueryHandler :
        IQueryHandler<GetBreadCrumbQuery, List<GetBreadCrumbDto>>
    {
        private readonly IEtebarDorehRepository _etebarDorehRepository;
        private readonly IMehvarRepository _mehvarRepository;
        private readonly IZirMehvarRepository _zirMehvarRepository;
        private readonly IStandardRepository  _standardRepository;
        private readonly ISanjehRepository  _sanjehRepository;
        private readonly IOrganizationRepository _organizationRepository;


        public GetBreadCrumbQueryHandler(IEtebarDorehRepository etebarDorehRepository, IMehvarRepository mehvarRepository, IZirMehvarRepository zirMehvarRepository, IStandardRepository standardRepository, ISanjehRepository sanjehRepository, IOrganizationRepository organizationRepository)
        {
            _etebarDorehRepository = etebarDorehRepository;
            _mehvarRepository = mehvarRepository;
            _zirMehvarRepository = zirMehvarRepository;
            _standardRepository = standardRepository;
            _sanjehRepository = sanjehRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<Result<List<GetBreadCrumbDto>>> Handle(GetBreadCrumbQuery request, CancellationToken cancellationToken)
        {


            if (request.Guid == null && request.GuidType is null)
            {
                return Result.Success(new List<GetBreadCrumbDto>());
            }

            if (request.GuidType.ToLower() == "orgtype")
            {
                var data = await _organizationRepository.FetchBreadCrumbDataAsync(request.Guid, cancellationToken);

                return data;
            }

            if (request.GuidType.ToLower()=="etebardoreh")
            {
                var data = await _etebarDorehRepository.FetchBreadCrumbDataAsync(request.Guid, cancellationToken);
               
                return data;
            }

            else if ((request.GuidType.ToLower() == "mehvar"))
            {
                var data = await _mehvarRepository.FetchBreadCrumbDataAsync(request.Guid, cancellationToken);

                return data;
            }

            else if ((request.GuidType.ToLower() == "zirmehvar"))
            {
                var data = await _zirMehvarRepository.FetchBreadCrumbDataAsync(request.Guid, cancellationToken);

                return data;
            }

            else if ((request.GuidType.ToLower() == "standard"))
            {
                var data = await _standardRepository.FetchBreadCrumbDataAsync(request.Guid, cancellationToken);

                return data;
            }

            else if ((request.GuidType.ToLower() == "sanjeh"))
            {
                var data = await _sanjehRepository.FetchBreadCrumbDataAsync(request.Guid, cancellationToken);

                return data;
            }

            return null;
        }
    }
}
