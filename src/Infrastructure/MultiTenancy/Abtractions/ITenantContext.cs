namespace multi_tenant_ca.Infrastructure.MultiTenancy.Abstractions;

public interface ITenantContext
{
    Guid? TenantId { get; }
    ITenantContext SetTenantId(string? tenantId);
}
