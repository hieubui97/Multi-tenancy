namespace multi_tenant_ca.Domain.Common;

public interface IMultiTenant
{
    Guid? TenantId { get; set; }
}
