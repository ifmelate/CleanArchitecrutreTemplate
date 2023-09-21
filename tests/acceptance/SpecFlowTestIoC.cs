using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProjectName.ServiceName.Acceptance.Tests.Helpers;
using ProjectName.ServiceName.Acceptance.Tests.Stubs.Contexts;
using ProjectName.ServiceName.Application.Common.Interfaces;
using ProjectName.ServiceName.Infrastructure.Persistence;
using SolidToken.SpecFlow.DependencyInjection;

// [assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace ProjectName.ServiceName.Acceptance.Tests;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
internal class SpecFlowTestIoC
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        RegisterBasicTestInfrastructure(services);
        
        // stubs registration

        return services;
    }

    private static void RegisterBasicTestInfrastructure(ServiceCollection services)
    {
        services.AddTransient<ITestDbContext, TestDbContext>();
        services.AddDbContext<TestDbContext>(options =>
            options.UseInMemoryDatabase("TestInMemoryDb"));
        services.AddTransient<IApplicationDbContext>(c => c.GetRequiredService<ITestDbContext>());

        services.AddSingleton<ITestDateTimeProvider, TestDateTimeProvider>();
        services.AddSingleton<IDateTimeProvider, TestDateTimeProvider>();
        services.AddLogging(config => { config.ClearProviders(); });


        services.AddSingleton<ITestContextDataRepository, TestContextDataRepository>();
    }
}
