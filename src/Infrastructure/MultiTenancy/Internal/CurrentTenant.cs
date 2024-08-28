
using multi_tenant_ca.Infrastructure.MultiTenancy.Abstractions;

namespace multi_tenant_ca.Infrastructure.MultiTenancy.Internal;

public class CurrentTenant : ICurrentTenant
{
    private Guid? _id;
    public Guid? Id { get => _id; private set => _id = value; }
}
