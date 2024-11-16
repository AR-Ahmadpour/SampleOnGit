using Accreditation.Application.Abstractions.Data;
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.EtebarDorehs;
using SharedKernel;

namespace Accreditation.Application.EnvironmentStandards.GetList
{
    internal sealed class GetEnvironmentStandardQueryHandler :
        IQueryHandler<GetListEnvironmentStandardQuery, List<GetListByEtebarDorehDto>>
    {
        IEtebarDorehRepository _etebarDorehRepository;
        IEnvironmentStandardRepository _environmentStandardRepository;

        public GetEnvironmentStandardQueryHandler(IEtebarDorehRepository etebarDorehRepository, IUnitOfWork unitOfWork, IEnvironmentStandardRepository environmentStandardRepository)
        {
            _etebarDorehRepository = etebarDorehRepository;
            _environmentStandardRepository = environmentStandardRepository;
        }

        public async Task<Result<List<GetListByEtebarDorehDto>>> Handle(GetListEnvironmentStandardQuery request, CancellationToken cancellationToken)
        {
            if (await _etebarDorehRepository.FindAsync(request.etebarDorehGuid, cancellationToken) is null)
            {
                return Result.Failure<List<GetListByEtebarDorehDto>>(EtebarDorehErrors.NotFound);
            }

            return await _environmentStandardRepository.GetListByEtebarDorehIdAsync(request.etebarDorehGuid, cancellationToken);
        }
    }
}
