using Microsoft.AspNetCore.Http;

namespace multi_tenant_ca.Infrastructure.Extensions;

public static class TenantContextExtension
{
    public static string GetHeaderValue(HttpContext context, string header)
    {
        return "";
    }

    public static string? GetClaimValue(HttpContext context, string claimName)
    {
        var claimValue = context.User.Claims.FirstOrDefault(x => x.Type == claimName)?.Value;
        return claimValue;
    }
}
