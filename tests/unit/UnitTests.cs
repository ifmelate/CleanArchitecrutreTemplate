using System.Security.Claims;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.ServiceName.Application.Common.Interfaces;
using Unit.Tests.Stubs;

namespace Unit.Tests;

public class UnitTests
{
    private readonly IDateTimeProvider _testDateTimeProvider;

    public UnitTests()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddScoped<IDateTimeProvider, TestDateTimeProvider>();
        var provider = services.BuildServiceProvider();
        _testDateTimeProvider = provider.GetRequiredService<IDateTimeProvider>();
    }

    [Fact]
    public void FirstTest1()
    {
        _testDateTimeProvider.UtcNow.Should().Be(DateTime.UtcNow);
    }

   
}
