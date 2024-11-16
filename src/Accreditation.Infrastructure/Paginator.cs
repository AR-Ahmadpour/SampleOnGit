using Accreditation.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Accreditation.Infrastructure;

internal class Paginator<T> : IPaginator<T>
{
    public int TotalItems { get; private set; }
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public IReadOnlyCollection<T> Items { get; private set; }
    public int TotalPages =>
          (int)Math.Ceiling(TotalItems / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    public int NextPageNumber =>
           HasNextPage ? PageNumber + 1 : TotalPages;
    public int PreviousPageNumber =>
           HasPreviousPage ? PageNumber - 1 : 1;

    public async Task<IPaginator<T>> Paginate(IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        TotalItems = await source.CountAsync();

        var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

        PageNumber = pageNumber;
        PageSize = pageSize;
        Items = items;
        return this;
    }

    public async Task<IPaginator<TNew>> TransformAsync<TNew>(
     Func<T, TNew> transform,
     CancellationToken cancellationToken)
    {
        var transformedItems = Items.Select(transform).ToList();
        return new Paginator<TNew>
        {
            TotalItems = TotalItems,
            PageNumber = PageNumber,
            PageSize = PageSize,
            Items = transformedItems
        };
    }

    public async Task<IReadOnlyCollection<T>> GetItems() => this.Items;
}
