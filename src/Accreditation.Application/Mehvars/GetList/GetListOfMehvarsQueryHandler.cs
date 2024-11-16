using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Persistence.Mehvars;
using Accreditation.Application.EtebarDorehs;
using SharedKernel;

namespace Accreditation.Application.Mehvars.GetList
{
    internal sealed class GetListOfMehvarsQueryHandler :
        IQueryHandler<GetListOfMehvarQuery, List<GetListOfMehvarsByEtebarDorehIdDto>>
    {
        private readonly IEtebarDorehRepository _etebarDorehRepository;
        private readonly IMehvarRepository _mehvarRepository;

        public GetListOfMehvarsQueryHandler(IEtebarDorehRepository etebarDorehRepository, IMehvarRepository mehvarRepository)
        {
            _etebarDorehRepository = etebarDorehRepository;
            _mehvarRepository = mehvarRepository;
        }

        public async Task<Result<List<GetListOfMehvarsByEtebarDorehIdDto>>> Handle(GetListOfMehvarQuery request, CancellationToken cancellationToken)
        {
            if (await _etebarDorehRepository.FindAsync(request.Etebardorehid, cancellationToken) is null)
            {
                return Result.Failure<List<GetListOfMehvarsByEtebarDorehIdDto>>(EtebarDorehErrors.NotFound);
            }


            return await _mehvarRepository.GetAllMehvarByEtebarDorehIdAsync(request.Etebardorehid,cancellationToken);
        }
    }
}
