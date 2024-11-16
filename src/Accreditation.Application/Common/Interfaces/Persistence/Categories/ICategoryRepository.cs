using Accreditation.Application.Category.CategorySelectedList;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Common.Interfaces.Persistence.Categories
{
    public interface ICategoryRepository
    {
        Task<Result<List<CategorySelectedListDto>>> CategorySelectedList(CancellationToken cancellationToken);
    }
}
