using Accreditation.Application.Abstractions.Messaging;
using Accreditation.Application.Common.Interfaces.Persistence.Categories;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Application.Category.CategorySelectedList
{
    public class CategorySelectedListHandler:IQueryHandler<CategorySelectedListQuery,List<CategorySelectedListDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategorySelectedListHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<List<CategorySelectedListDto>>> Handle(CategorySelectedListQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.CategorySelectedList(cancellationToken);
       }         
    }
}
