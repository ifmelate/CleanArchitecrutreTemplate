
using NSwag;
using NSwag.Generation.Processors.Security;
using ProjectName.ServiceName.Application;
using ProjectName.ServiceName.Infrastructure;
using ZymLabs.NSwag.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();

builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddOpenApiDocument((configure, serviceProvider) =>
{
    var fluentValidationSchemaProcessor = serviceProvider.CreateScope().ServiceProvider
        .GetRequiredService<FluentValidationSchemaProcessor>();

    // Add the fluent validations schema processor
    configure.SchemaProcessors.Add(fluentValidationSchemaProcessor);

    configure.Title = "ProjectName.ServiceName API";
    configure.AddSecurity("JWT", Enumerable.Empty<string>(),
        new OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Type into the textbox: Bearer {your JWT token}."
        });

    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi3(settings =>
{
    settings.Path = "/api/swagger";
    settings.DocumentPath = "/api/specification.json";
});

app.UseRouting();

#pragma warning disable S125
//TODO: use app.UseAuthentication();
//TODO: use app.UseAuthorization();
#pragma warning restore S125

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();
