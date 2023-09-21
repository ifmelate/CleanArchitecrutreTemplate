using ProjectName.ServiceName.Acceptance.Tests.Base;

namespace ProjectName.ServiceName.Acceptance.Tests.Hooks;

[Binding]
internal class Hooks
{
    [BeforeFeature]
    public static void BeforeFeatureRun()
    {
    }

    [BeforeScenario]
    public void BeforeScenario(ScenarioContext scenario)
    {
        TestThreadHelper.CatchTestThreadIfNotExist(scenario);
    }

    [BeforeStep]
    public void BeforeStep(ScenarioContext scenario)
    {
        TestThreadHelper.AddStepThreadInfoToConsole(scenario);
    }

    [AfterStep]
    public void AfterStep(ScenarioContext scenario)
    {
        TestThreadHelper.AddStepThreadInfoToConsole(scenario, isEndStep: true);
    }

    [AfterTestRun]
    public static void AfterTestRun(ScenarioContext scenario)
    {
        TestThreadHelper.AddTestThreadsToOutput();
    }
}
