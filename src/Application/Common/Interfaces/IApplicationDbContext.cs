using multi_tenant_ca.Domain.Entities;

namespace multi_tenant_ca.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
