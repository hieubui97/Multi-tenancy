using System.Reflection;
using multi_tenant_ca.Application.Common.Interfaces;
using multi_tenant_ca.Domain.Entities;
using multi_tenant_ca.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using multi_tenant_ca.Domain.Common;
using multi_tenant_ca.Infrastructure.Data.Configurations;
using multi_tenant_ca.Infrastructure.MultiTenancy.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace multi_tenant_ca.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly ITenantContext? _tenantContext;


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantContext tenantContext) : base(options)
    {
        _tenantContext = tenantContext;
    }

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.ApplyConfiguration(new BookConfiguration());

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(IMultiTenant).IsAssignableFrom(entityType.ClrType))
            {
                builder.Entity(entityType.ClrType).HasQueryFilter(GetFilter(entityType, nameof(GetTenantFilterExpression), _tenantContext?.TenantId));
            }
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                builder.Entity(entityType.ClrType).HasQueryFilter(GetFilter(entityType, nameof(GetDataFilterExpression)));
            }
        }
    }

    private static LambdaExpression GetFilter(IMutableEntityType entityType, string methodName, params object?[]? parameters)
    {
        var method = typeof(ApplicationDbContext).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static)!
                                                 .MakeGenericMethod(entityType.ClrType);
        return (LambdaExpression)method.Invoke(null, parameters)!;
    }

    private static Expression<Func<T, bool>> GetTenantFilterExpression<T>(Guid? tenantId) where T : IMultiTenant
    {
        return e => e.TenantId == tenantId;
    }

    private static Expression<Func<T, bool>> GetDataFilterExpression<T>() where T : ISoftDelete
    {
        return e => !e.IsDeleted;
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<IMultiTenant>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.TenantId = _tenantContext?.TenantId;
            }
        }

        return base.SaveChanges();
    }
}
