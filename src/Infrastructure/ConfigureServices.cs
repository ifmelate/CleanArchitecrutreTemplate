using Confluent.Kafka;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.ServiceName.Application.Common.Interfaces;
using ProjectName.ServiceName.Domain.Events;
using ProjectName.ServiceName.Infrastructure.Consumers;
using ProjectName.ServiceName.Infrastructure.Persistence;
using ProjectName.ServiceName.Infrastructure.Persistence.Interceptors;
using ProjectName.ServiceName.Infrastructure.Services;

namespace ProjectName.ServiceName.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("ProjectName.ServiceName.MemoryDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddAuthentication();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        RegisterKafka(services);
        
        return services;
    }

    private static void RegisterKafka(IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));
            x.AddRider(rider =>
            {
                rider.AddConsumer<TestConsumer>();
                rider.UsingKafka((context, k) =>
                {
                    k.Host("localhost:9094");
                    k.ClientId = "backend";
                    k.TopicEndpoint<string, TestEvent>("test-topic2", "test-group-consumer",
                        e =>
                        {
                            e.CheckpointInterval = TimeSpan.FromSeconds(10);
                            e.AutoOffsetReset = AutoOffsetReset.Earliest;
                            e.ConfigureConsumer<TestConsumer>(context);
                        });
                });
            });
        });
    }
}
