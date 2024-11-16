using Accreditation.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Accrediation.Application.Common.Models
{
    public class PagingParams
    {
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}!")]
        public int PageNumber { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}!")]
        public int PageSize { get; set; } = 5;
    }

    public class PagedList<T> : IPagedList<T>
    {
        public PagedList(
            int totalItems,
            IReadOnlyCollection<T> items,
            int pageNumber,
            int pageSize)
        {
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = items;
        }



        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public IReadOnlyCollection<T> Items { get; }
        public int TotalPages =>
              (int)Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int NextPageNumber =>
               HasNextPage ? PageNumber + 1 : TotalPages;
        public int PreviousPageNumber =>
               HasPreviousPage ? PageNumber - 1 : 1;

        public async Task<PagedList<T>> Paginate(
       IQueryable<T> source,
       int pageNumber,
       int pageSize,
       CancellationToken cancellationToken)
        {
            var totalItems = await source.CountAsync();
            var items = await source
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

            return new PagedList<T>(totalItems, items, pageNumber, pageSize);
        }
    }
}
