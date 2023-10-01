using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjectName.ServiceName.WebApi.Swagger;

public static class SwaggerConfigureOptions
{

    /// <summary>
    /// Enable api versioning and apply configure swagger default configuration with versioning documents
    /// </summary>
    /// <param name="services"></param>
    /// <param name="defaultApiVersion"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureVersioningWithSwagger(this IServiceCollection services,
        int defaultApiVersion)
    {
        return services.Configure<ConfigureSwaggerOptions, DefaultExampleSchemaFilter>(defaultApiVersion);
    }


    private static IServiceCollection Configure<TConfigureSettings, TSchemaFilter>(this IServiceCollection services, int defaultApiVersion) 
        where TConfigureSettings : class
        where TSchemaFilter: ISchemaFilter
    {
        services.AddEndpointsApiExplorer();
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(defaultApiVersion,0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
        });

        services.AddSwaggerGen(c => { c.SchemaFilter<TSchemaFilter>(); }); 
    
       
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        services.ConfigureOptions<TConfigureSettings>();
        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, string servicePrefixName,IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        app.UseSwagger(c =>
        {
            c.RouteTemplate = "/api/swagger/{documentName}/swagger.{json|yaml}";
        });
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
            {
                options.SwaggerEndpoint($"/api/swagger/{description.GroupName}/swagger.json",
                    $"{servicePrefixName.ToUpperInvariant()} {description.GroupName.ToUpperInvariant()}");
            }
            options.RoutePrefix = $"api/swagger";
        });
        return app;
    }
}
