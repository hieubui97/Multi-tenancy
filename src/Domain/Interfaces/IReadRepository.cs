using Ardalis.Specification;

public interface IReadRepository<T>
{
    IQueryable<T> GetQueryable();

    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

    Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    Task<T?> SingleOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<TResult?> SingleOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    Task<List<T>> GetListAsync(CancellationToken cancellationToken = default);
    Task<List<T>> GetListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<List<TResult>> GetListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    Task<List<TResult>> ProjectToListAsync<TResult>(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<TResult?> ProjectToFirstOrDefaultAsync<TResult>(ISpecification<T> specification, CancellationToken cancellationToken);
}
