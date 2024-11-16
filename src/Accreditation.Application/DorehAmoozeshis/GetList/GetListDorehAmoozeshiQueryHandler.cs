using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.DorehAmoozeshis;
using SharedKernel;

namespace Accreditation.Application.DorehAmoozeshis.GetList
{
    internal sealed class GetListDorehAmoozeshiQueryHandler :
        IQueryHandler<GetListDorehAmoozeshiQuery, List<GetListDorehAmoozeshiDto>>
    {
        private readonly IDorehAmoozeshiRepository _dorehAmoozeshiRepository;

        public GetListDorehAmoozeshiQueryHandler(IDorehAmoozeshiRepository dorehAmoozeshiRepository)
        {
            _dorehAmoozeshiRepository = dorehAmoozeshiRepository;
        }

        public async Task<Result<List<GetListDorehAmoozeshiDto>>> Handle(GetListDorehAmoozeshiQuery request, CancellationToken cancellationToken)
        {
            return await _dorehAmoozeshiRepository.GetListDorehAmoozeshiAsync(cancellationToken);
        }
    }
}
