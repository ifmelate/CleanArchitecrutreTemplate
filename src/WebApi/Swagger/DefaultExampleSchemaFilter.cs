using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjectName.ServiceName.WebApi.Swagger;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class DefaultExampleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
    }
}
