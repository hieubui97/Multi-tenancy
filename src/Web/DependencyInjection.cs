using Azure.Identity;
using multi_tenant_ca.Application.Common.Interfaces;
using multi_tenant_ca.Infrastructure.Data;
using multi_tenant_ca.Web.Services;
using Microsoft.AspNetCore.Mvc;
using multi_tenant_ca.Infrastructure.MultiTenancy.Abstractions;
using multi_tenant_ca.Infrastructure.MultiTenancy.Internal;
using multi_tenant_ca.Infrastructure.MultiTenancy.Option;


namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ITenantContext, TenantContext>();
        services.AddSingleton<MultiTenancyOption>(_ => new MultiTenancyOption { SkipPaths = ["Identity", "swagger", "index.html"] });

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "multi_tenant_ca API";
        });

        return services;
    }

    public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services, ConfigurationManager configuration)
    {
        var keyVaultUri = configuration["KeyVaultUri"];
        if (!string.IsNullOrWhiteSpace(keyVaultUri))
        {
            configuration.AddAzureKeyVault(
                new Uri(keyVaultUri),
                new DefaultAzureCredential());
        }

        return services;
    }
}
