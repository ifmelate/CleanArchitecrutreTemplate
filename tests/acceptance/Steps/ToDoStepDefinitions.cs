using ProjectName.ServiceName.Acceptance.Tests.Base;
using ProjectName.ServiceName.Acceptance.Tests.Helpers;
using ProjectName.ServiceName.Application.Common.Interfaces;
using ProjectName.ServiceName.Infrastructure.Persistence;

namespace ProjectName.ServiceName.Acceptance.Tests.Steps;

[Binding]
internal sealed class ToDoStepDefinitions: BaseStepDefinitions<ApplicationDbContext, ITestContextDataRepository>
{
    private readonly ITestDateTimeProvider _dateTimeProvider;

    public ToDoStepDefinitions(ITestContextDataRepository testContextDataRepository,
        ITestDateTimeProvider dateTimeProvider): base(testContextDataRepository)
    {
        _dateTimeProvider = dateTimeProvider;
    }

   
}
