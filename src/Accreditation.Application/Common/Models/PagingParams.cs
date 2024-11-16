using System.ComponentModel.DataAnnotations;


namespace Accreditation.Application.Common.Models;

public class PagingParams
{
    [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}!")]
    public int PageNumber { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}!")]
    public int PageSize { get; set; } = 10;
}

public class PagedList<T>
{
    public PagedList(int totalItems, IReadOnlyCollection<T> items, int pageNumber, int pageSize)
    {
        TotalItems = totalItems;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Items = items;
    }

    public static async Task<PagedList<T>> Paginate(
        IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        // Fetch the items for the current page
        var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsEnumerable()
            .ToList();

        // Get the total number of items
        var totalItems = source.AsEnumerable().Count();

        return new PagedList<T>(totalItems, items, pageNumber, pageSize);
    }

    public int TotalItems { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public IReadOnlyCollection<T> Items { get; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    public int NextPageNumber => HasNextPage ? PageNumber + 1 : TotalPages;
    public int PreviousPageNumber => HasPreviousPage ? PageNumber - 1 : 1;
}
