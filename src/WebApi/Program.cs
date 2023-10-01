using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ProjectName.ServiceName.Application;
using ProjectName.ServiceName.Infrastructure;
using ProjectName.ServiceName.WebApi.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();

builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.ConfigureVersioningWithSwagger(1, "ProjectName.ServiceName");


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

app.UseSwaggerConfiguration(app.Services.GetRequiredService<IApiVersionDescriptionProvider>());
app.UseRouting();

#pragma warning disable S125
//TODO: use app.UseAuthentication();
//TODO: use app.UseAuthorization();
#pragma warning restore S125

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");


app.Run();
