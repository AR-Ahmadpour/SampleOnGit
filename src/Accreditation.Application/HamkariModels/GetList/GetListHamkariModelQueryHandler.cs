using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.HamkariModels;
using SharedKernel;

namespace Accreditation.Application.HamkariModels.GetList
{
    internal sealed class GetListHamkariModelQueryHandler :
        IQueryHandler<GetListHamkariModelQuery, List<GetListHamkariModelDto>>
    {
        private readonly IHamkariModelRepository _hamkariModel;

        public GetListHamkariModelQueryHandler(IHamkariModelRepository hamkariModel)
        {
            _hamkariModel = hamkariModel;
        }

        public async Task<Result<List<GetListHamkariModelDto>>> Handle(GetListHamkariModelQuery request, CancellationToken cancellationToken)
        {
            return await _hamkariModel.GetListAsync(cancellationToken);
        }
    }
}
