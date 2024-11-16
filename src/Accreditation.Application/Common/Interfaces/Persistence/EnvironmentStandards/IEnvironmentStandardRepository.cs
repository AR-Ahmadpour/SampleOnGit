using Accreditation.Domain.EnvironmentStandards.Entities;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Common.Interfaces.Persistence.EnvironmentStandards
{
    public interface IEnvironmentStandardRepository
    {
        Task<List<SelectListResponse>> GetSelectList(CancellationToken cancellationToken = default);
        Task<List<SelectListResponse>> GetSelectListAsync(Guid etebarDoreGuid);
        List<Guid> GetGuidList(Guid etebarDoreGuid);
        Task<List<GetListByEtebarDorehDto>> GetListByEtebarDorehIdAsync(Guid etebarDorehGuid, CancellationToken cancellationToken = default);
        Task<bool> FindAsync(Guid guids, CancellationToken cancellationToken = default);
    }
}
