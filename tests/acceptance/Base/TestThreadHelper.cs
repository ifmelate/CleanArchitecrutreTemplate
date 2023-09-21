using System.Collections.Concurrent;
using Dumpify;

namespace ProjectName.ServiceName.Acceptance.Tests.Base;

public static class TestThreadHelper
{
    private static readonly ConcurrentDictionary<int, List<string>> TestThreads = new();

    public static void AddTestThreadsToOutput()
    {
        Console.WriteLine($"List of test threads:");
        Console.WriteLine($"{TestThreads.OrderBy(x => x.Key).Dump()}");
        TestThreads.Clear();
    }

    public static void CatchTestThreadIfNotExist(ScenarioContext scenario)
    {
        var testName = scenario.ScenarioInfo.Title;
        var threadId = GetThreadId();
        if (string.IsNullOrEmpty(testName) || threadId == 0)
            return;

        if (TestThreads.ContainsKey(threadId))
        {
            var tests = TestThreads[threadId];
            if (tests.Contains(testName))
                return;

            tests.Add(testName);
            TestThreads[threadId] = tests;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Start thread [{threadId}] for test '{testName}'");
        }
        else
        {
            TestThreads.TryAdd(threadId, new List<string> {testName});
        }
    }
        
    private static int GetThreadId()
    {
        var threadId = Thread.CurrentThread.ManagedThreadId;
        return threadId;
    }

    public static void AddStepThreadInfoToConsole(ScenarioContext scenario, bool isEndStep = false)
    {
        var stepInfo = scenario.StepContext.StepInfo;
        var stepText = $"{stepInfo.StepDefinitionType} {stepInfo.Text}";
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{(!isEndStep ? "Start": "End")} step[{GetThreadId()}]: {stepText}");
    }
}