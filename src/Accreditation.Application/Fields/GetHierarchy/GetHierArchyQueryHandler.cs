using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.EvaluationArzyabs;
using Accreditation.Application.Mehvars;
using SharedKernel;

namespace Accreditation.Application.Fields.GetHierarchy
{
    internal sealed class GetHierArchyQueryHandler :
        IQueryHandler<GetListHierArchyQuery, List<HierarchyNodeDto>>
    {
        private readonly IFieldRepository _fieldRepository;

        public GetHierArchyQueryHandler(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }

        public async Task<Result<List<HierarchyNodeDto>>> Handle(GetListHierArchyQuery request, CancellationToken cancellationToken)
        {
            var mehvar = await _fieldRepository.GetMehvarByIdAsync(request.mehvarId);

            if (mehvar is null)
            {
                return Result.Failure<List<HierarchyNodeDto>>(MehvarErrors.NotFound);
            }


            var zirmehvarNodes = await FetchZirmehvarNodesAsync(mehvar.GUID);
            return Result.Success(zirmehvarNodes);
        }

        private async Task<List<HierarchyNodeDto>> FetchZirmehvarNodesAsync(Guid mehvarGuid)
        {
            var zirmehvarEntities = await _fieldRepository.GetZirmehvarsByMehvarGuidAsync(mehvarGuid);
            var zirmehvarNodes = new List<HierarchyNodeDto>();

            foreach (var zirmehvar in zirmehvarEntities)
            {
                var zirmehvarNode = new HierarchyNodeDto
                {
                    Title = zirmehvar.Title,
                    Level = 1, // Set level for Zirmehvar
                    Guid = null // No Guid for Zirmehvar
                };

                var standardNodes = await FetchStandardNodesAsync(zirmehvar.GUID);
                zirmehvarNode.Children.AddRange(standardNodes);
                zirmehvarNodes.Add(zirmehvarNode);
            }

            return zirmehvarNodes;
        }

        private async Task<List<HierarchyNodeDto>> FetchStandardNodesAsync(Guid zirmehvarGuid)
        {
            var standardEntities = await _fieldRepository.GetStandardsByZirmehvarIdAsync(zirmehvarGuid);
            var standardNodes = new List<HierarchyNodeDto>();

            foreach (var standard in standardEntities)
            {
                var standardNode = new HierarchyNodeDto
                {
                    Title = standard.Title,
                    Level = 2, // Set level for Standard
                    Guid = null // No Guid for Standard
                };

                var sanjehNodes = await FetchSanjehNodesAsync(standard.GUID);
                standardNode.Children.AddRange(sanjehNodes);
                standardNodes.Add(standardNode);
            }

            return standardNodes;
        }

        private async Task<List<HierarchyNodeDto>> FetchSanjehNodesAsync(Guid standardGuid)
        {
            var sanjehEntities = await _fieldRepository.GetSanjehsByStandardIdAsync(standardGuid);
            var sanjehNodes = new List<HierarchyNodeDto>();

            foreach (var sanjeh in sanjehEntities)
            {
                var sanjehNode = new HierarchyNodeDto
                {
                    Title = sanjeh.Title,
                    Guid = sanjeh.GUID, 
                    Level = 3 
                };

                sanjehNodes.Add(sanjehNode);
            }

            return sanjehNodes;
        }
    }
}
