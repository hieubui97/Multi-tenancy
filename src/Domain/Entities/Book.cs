
namespace multi_tenant_ca.Domain.Entities;

public class Book : BaseAuditableEntity, IMultiTenant
{
    public string? Name { get; set; }
    public float Price { get; set; }
    public Guid? TenantId { get; set; }
}
