using Accreditation.Application.Category.CategorySelectedList;
using Accreditation.Application.Common.Interfaces.Persistence.Categories;
using Accreditation.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Infrastructure.Repositories
{
    public sealed class CategoryRepository: ICategoryRepository
    {
        private readonly AccreditationDbContext _context;

        public CategoryRepository(AccreditationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<CategorySelectedListDto>>> CategorySelectedList(CancellationToken cancellationToken)
        {
            var CategorySelectedList= await _context.Categories
                .Where(category => category.IsDeleted == false)
                .Select(category => new CategorySelectedListDto()
                { 
                    Id =category.Id,
                    Title=category.Title
                }
                )
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            CategorySelectedListDto temp = new CategorySelectedListDto();
            temp.Id = -1;
            temp.Title = "انتخاب کنید";
            CategorySelectedList.Insert(0, temp);
            return CategorySelectedList;
        }
    }
}
