namespace multi_tenant_ca.Infrastructure.MultiTenancy.Abstractions;

public interface ICurrentTenant
{
    Guid? Id { get; }
}
