using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProjectName.ServiceName.Application.Common.Interfaces;
using ProjectName.ServiceName.Infrastructure.Persistence;
using ProjectName.ServiceName.Infrastructure.Persistence.Interceptors;

namespace ProjectName.ServiceName.Acceptance.Tests.Stubs.Contexts;

public interface ITestDbContext: IApplicationDbContext
{
}
internal class TestDbContext: ApplicationDbContext, ITestDbContext
{
    private readonly ScenarioContext _scenarioContext;

    public TestDbContext(AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor, ScenarioContext scenarioContext, ITestContextDataRepository testContextDataRepository) : base(auditableEntitySaveChangesInterceptor)
    {
        _scenarioContext = scenarioContext;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase($"TestInMemoryDb_{_scenarioContext.ScenarioInfo.Title}");
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
        optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
}
