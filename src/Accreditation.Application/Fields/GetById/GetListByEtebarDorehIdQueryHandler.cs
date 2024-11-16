using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EtebarDorehs;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Application.Common.Interfaces.Persistence.Sanjehs;
using Accreditation.Application.Common.Models;
using Accreditation.Application.EtebarDorehs;
using SharedKernel;

namespace Accreditation.Application.Fields.GetById
{
    internal sealed class GetListByEtebarDorehIdQueryHandler :
        IQueryHandler<GetlistByEtebarDorehIdQuery, PagedList<GetListByEtebarDorehIdDto>>
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IEtebarDorehRepository _etebarDorehRepository;

        public GetListByEtebarDorehIdQueryHandler(IFieldRepository fieldRepository,
            IEtebarDorehRepository etebarDorehRepository)
        {
            _fieldRepository = fieldRepository;
            _etebarDorehRepository = etebarDorehRepository;
        }

        public async Task<Result<PagedList<GetListByEtebarDorehIdDto>>> Handle(GetlistByEtebarDorehIdQuery request, CancellationToken cancellationToken)
        {

            var etebarDoreh = await _etebarDorehRepository.FindAsync(request.EtebardorehGuid, cancellationToken);

            if (etebarDoreh is null)
            {
                return Result.Failure<PagedList<GetListByEtebarDorehIdDto>>(EtebarDorehErrors.NotFound);
            }

            return await _fieldRepository.GetListByEtebarDorehIdAsync(request.EtebardorehGuid,request.PageNumber,request.Pagesize, cancellationToken);


        }
    }
}
