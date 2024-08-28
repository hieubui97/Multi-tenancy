using Microsoft.AspNetCore.Identity;
using multi_tenant_ca.Domain.Common;

namespace multi_tenant_ca.Infrastructure.Identity;

public class ApplicationUser : IdentityUser, IMultiTenant
{
    public Guid? TenantId { get; set; }
}
