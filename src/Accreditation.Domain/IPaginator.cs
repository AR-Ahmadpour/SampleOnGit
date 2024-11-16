namespace Accreditation.Application.Abstractions;

public interface IPaginator<T>
{
    Task<IPaginator<T>> Paginate(
        IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    Task<IPaginator<TNew>> TransformAsync<TNew>(
    Func<T, TNew> transform,
    CancellationToken cancellationToken);

    Task<IReadOnlyCollection<T>> GetItems();
}
