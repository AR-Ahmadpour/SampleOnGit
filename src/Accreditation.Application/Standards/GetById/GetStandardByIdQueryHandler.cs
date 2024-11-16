
using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Standards;
using SharedKernel;

namespace Accreditation.Application.Standards.GetById
{
    internal sealed class GetStandardrByIdQueryHandler
    : IQueryHandler<GetStandardByIdQuery, GetStandardResponse>
    {
        private readonly IStandardRepository _standardRepository;

        public GetStandardrByIdQueryHandler(IStandardRepository standardRepository)
        {
            _standardRepository = standardRepository;
        }
        public async Task<Result<GetStandardResponse>> Handle(GetStandardByIdQuery request, CancellationToken cancellationToken)
        {
            var standard = await _standardRepository.GetByIdAsync(request.GUID, cancellationToken);

            if (standard == null)
            {
                return Result.Failure<GetStandardResponse>(StandardErrors.NotFound);
            }

            var response = new GetStandardResponse
            {
                Guid = standard.GUID,
                Title = standard.Title,
                ShortTitle = standard.ShortTitle,
                Code = standard.Code,
                SortOrder = standard.SortOrder,
                WeightedCoefficient = standard.WeightedCoefficient,
                IsDeleted = standard.IsDeleted
            };

            return response;

        }


    }
}
