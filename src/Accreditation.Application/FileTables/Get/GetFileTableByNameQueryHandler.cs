using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.FileTables;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.FileTables.Get
{
    internal class GetFileTableByNameQueryHandler
        :IQueryHandler<GetFileTableByNameQuery, GetFileTableByNameDto>
    {
        private readonly IFileTableRepository _fileTableRepository;

        public GetFileTableByNameQueryHandler(IFileTableRepository fileTableRepository)
        {
            _fileTableRepository = fileTableRepository;
        }

        public async Task<Result<GetFileTableByNameDto>> Handle(GetFileTableByNameQuery request, CancellationToken cancellationToken)
        {
           return await _fileTableRepository.GetByName(request.FileName, cancellationToken);  
        }
    }
}
