
using multi_tenant_ca.Infrastructure.MultiTenancy.Abstractions;

namespace multi_tenant_ca.Infrastructure.MultiTenancy.Internal;

public class TenantContext : ITenantContext
{
    private Guid? _tenantId;
    public Guid? TenantId { get => _tenantId; private set => _tenantId = value; }

    public ITenantContext SetTenantId(string? tenantId)
    {
        _tenantId = Guid.TryParse(tenantId, out var id) ? id : null;
        return this;
    }
}
