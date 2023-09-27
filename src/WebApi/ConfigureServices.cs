using Microsoft.AspNetCore.Mvc;
using ProjectName.ServiceName.Application.Common.Interfaces;
using ProjectName.ServiceName.WebApi.Services;
using ZymLabs.NSwag.FluentValidation;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks();
#pragma warning disable S125
        //TODO: use     .AddDbContextCheck<ApplicationDbContext>();
#pragma warning restore S125

        services.AddControllers();


        services.AddScoped<FluentValidationSchemaProcessor>(provider =>
        {
            var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            var loggerFactory = provider.GetService<ILoggerFactory>();

            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

       

        return services;
    }
}
