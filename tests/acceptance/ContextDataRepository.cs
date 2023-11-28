using System.Diagnostics.CodeAnalysis;
using ProjectName.ServiceName.Acceptance.Tests.Base;

namespace ProjectName.ServiceName.Acceptance.Tests;

internal interface ITestContextDataRepository: ITestBaseContextDataRepository
{
  
}

[SuppressMessage("ReSharper", "RedundantAssignment")]
internal class TestContextDataRepository: TestBaseContextDataRepository, ITestContextDataRepository
{
    private readonly ScenarioContext _scenarioContext;

    public TestContextDataRepository(ScenarioContext scenarioContext): base(scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    
}
