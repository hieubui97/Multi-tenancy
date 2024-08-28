using Microsoft.AspNetCore.Builder;
using multi_tenant_ca.Infrastructure.MultiTenancy.Middlewares;

namespace multi_tenant_ca.Infrastructure.Extensions;

public static class MultiTenancyApplicationBuilderExtension
{
    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app)
    {
        return app.UseMiddleware<MultiTenancyMiddleware>();
    }
}
