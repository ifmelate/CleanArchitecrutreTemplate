using System.Collections.Concurrent;
using ProjectName.ServiceName.Application.Common.Interfaces;

namespace ProjectName.ServiceName.Acceptance.Tests.Helpers;

internal interface ITestDateTimeProvider: IDateTimeProvider
{
    void SetDateTimeNow(DateTime dateTime);
}
internal class TestDateTimeProvider : ITestDateTimeProvider
{
    private readonly ScenarioContext _scenarioContext;
    private static readonly ConcurrentDictionary<string, DateTime> DateTimeNowToTest = new();

    public TestDateTimeProvider(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    private static Func<DateTime> _getDateTimeNow = () => DateTime.UtcNow;

    public DateTime UtcNow
    {
        get
        {
            var testName = _scenarioContext.ScenarioInfo.Title;

            DateTimeNowToTest.TryAdd(testName, _getDateTimeNow());

            return DateTimeNowToTest[testName];
        }
    }

    public void SetDateTimeNow(DateTime dateTime)
    {
        var testName = _scenarioContext.ScenarioInfo.Title;
        DateTimeNowToTest.TryAdd(testName, dateTime);
        DateTimeNowToTest[testName] = dateTime;
    }

    public static void ResetDateTime()
    {
        _getDateTimeNow = () => DateTime.UtcNow;
    }
}
