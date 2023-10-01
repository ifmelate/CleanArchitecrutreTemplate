using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjectName.ServiceName.WebApi.Swagger;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class ConfigureSwaggerOptions
    : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly SwaggerDisplayTitle _swaggerDisplayTitle;

    public ConfigureSwaggerOptions(
        IApiVersionDescriptionProvider provider, SwaggerDisplayTitle swaggerDisplayTitle)
    {
        _provider = provider;
        _swaggerDisplayTitle = swaggerDisplayTitle;
    }

    /// <summary>
    /// Configure each API discovered for Swagger Documentation
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerGenOptions options)
    {
        // add swagger document for every API version discovered
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));
        }
        options.EnableAnnotations();
           
        options.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
        {
            Description = "Api key needed to access the endpoints. X-Api-Key: My_API_Key",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
           
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Authorization"
                    },
                },
                new string[] { }
            }
        });
    }


    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }


    private OpenApiInfo CreateVersionInfo(
        ApiVersionDescription desc)
    {
        var info = new OpenApiInfo
        {
            Title = $"{_swaggerDisplayTitle.Title} API",
            Version = desc.ApiVersion.ToString()
        };

        if (desc.IsDeprecated)
        {
            info.Description += " This API version has been deprecated. Please use one of the new APIs available from the explorer.";
        }

        return info;
    }
}
