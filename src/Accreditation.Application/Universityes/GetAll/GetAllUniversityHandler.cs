using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Universityes;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Universityes.GetAll
{
    public class GetAllUniversityHandler
        : IQueryHandler<GetAllUniversityQuery, List<GetAllUniversityDto>>
    {
        private readonly IUniversityRepository _universityRepository;

        public GetAllUniversityHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task<Result<List<GetAllUniversityDto>>> Handle(GetAllUniversityQuery request, CancellationToken cancellationToken)
        {
            return await _universityRepository.GetUniversity(request, cancellationToken);
        }
    }
}
