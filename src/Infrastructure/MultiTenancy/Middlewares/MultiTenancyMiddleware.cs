using System.Net;
using Microsoft.AspNetCore.Http;
using multi_tenant_ca.Infrastructure.Constants;
using multi_tenant_ca.Infrastructure.Extensions;
using multi_tenant_ca.Infrastructure.MultiTenancy.Abstractions;
using multi_tenant_ca.Infrastructure.MultiTenancy.Option;

namespace multi_tenant_ca.Infrastructure.MultiTenancy.Middlewares;

public class MultiTenancyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly MultiTenancyOption _options;

    public MultiTenancyMiddleware(RequestDelegate next, MultiTenancyOption options)
    {
        _next = next;
        _options = options;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.HasValue && _options.SkipPaths != null && _options.SkipPaths.Any())
        {
            var path = context.Request.Path.Value;
            var shouldSkip = _options.SkipPaths.Any(x => path.Contains(x));
            if (shouldSkip)
            {
                await _next(context);
                return;
            }
        }

        if (context.User.Identity != null && !context.User.Identity.IsAuthenticated)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }

        var tenantContext = context.RequestServices.GetService(typeof(ITenantContext)) as ITenantContext;

        var tenantId = GetHeaderAndClaimValue(context, HeaderConstant.TENANT_KEY, ClaimConstant.TENANT_ID);

        //if (string.IsNullOrEmpty(tenantId))
        //{
        //    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        //    return;
        //}

        tenantContext?.SetTenantId(tenantId);

        await _next(context);
        return;
    }

    private string? GetHeaderAndClaimValue(HttpContext context, string header, string claimName)
    {
        if (CanGetFromHeader(context))
            return "";

        return TenantContextExtension.GetClaimValue(context, claimName);
    }

    private bool CanGetFromHeader(HttpContext context) => context.User.Claims.Any(x => x.Type == "allowHeader");
}
