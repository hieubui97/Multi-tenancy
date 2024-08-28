using Ardalis.SharedKernel;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace multi_tenant_ca.Infrastructure.Data;

// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    private readonly DbContext _dbContext;
    protected readonly AutoMapper.IConfigurationProvider _configurationProvider;

    public EfRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _dbContext = dbContext;
        _configurationProvider = mapper.ConfigurationProvider;
    }

    public IQueryable<T> GetQueryable()
    {
        return _dbContext.Set<T>().AsQueryable();
    }

    public async Task<TResult?> ProjectToFirstOrDefaultAsync<TResult>(ISpecification<T> specification, CancellationToken cancellationToken)
    {
        return await ApplySpecification(specification)
            .ProjectTo<TResult>(_configurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<TResult>> ProjectToListAsync<TResult>(ISpecification<T> specification, CancellationToken cancellationToken)
    {
        return await ApplySpecification(specification)
            .ProjectTo<TResult>(_configurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<T?> SingleOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult?> SingleOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<List<T>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public virtual async Task<List<T>> GetListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

        return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
    }

    public virtual async Task<List<TResult>> GetListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

        return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
    }
}
