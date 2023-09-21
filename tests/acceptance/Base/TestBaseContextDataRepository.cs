#pragma warning disable CS8603 // Possible null reference return.

namespace ProjectName.ServiceName.Acceptance.Tests.Base;

public interface ITestBaseContextDataRepository
{
    List<Exception>? Exceptions { get; set; }
    T? GetPropertyByNameFromContext<T>(string propertyName);
    
}

public class TestBaseContextDataRepository: ITestBaseContextDataRepository
{
    private readonly ScenarioContext _scenarioContext;

    public TestBaseContextDataRepository(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    public T? GetPropertyByNameFromContext<T>(string propertyName)
    {
        var isExist = _scenarioContext.TryGetValue($"{_scenarioContext.ScenarioInfo.Title}_{propertyName}", out T value);
        if (isExist)
        {
            return value;
        }
        return (T?) (object?) null;
    }

    private void SaveToContext<T>(T obj, string keyName)
    {
        _scenarioContext.Set(obj, $"{_scenarioContext.ScenarioInfo.Title}_{keyName}");
    }
    
    public List<Exception>? Exceptions
    {
        get => GetPropertyByNameFromContext<List<Exception>?>(nameof(Exceptions));
        set => SaveToContext(value, nameof(Exceptions));
    }

}
