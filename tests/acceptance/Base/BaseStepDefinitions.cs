using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;

namespace ProjectName.ServiceName.Acceptance.Tests.Base;

public class BaseStepDefinitions<TDbContext, TContextData>
    where TDbContext: DbContext 
    where TContextData: ITestBaseContextDataRepository
{
    protected readonly TContextData _contextDataRepository;

    protected BaseStepDefinitions(TContextData contextDataRepository)
    {
        _contextDataRepository = contextDataRepository;
    }
    protected void SaveExceptionToContext(Exception exception)
    {
        List<Exception> exceptions;
        if (_contextDataRepository.Exceptions is null)
        {
            exceptions = new List<Exception>();
        }
        else
        {
            exceptions = _contextDataRepository.Exceptions;
        }
        exceptions.Add(exception);
        _contextDataRepository.Exceptions = exceptions;
    }
    
    protected void DoesNotThrowExceptionAssertion()
    {
        var isExist = _contextDataRepository.Exceptions is not null;
        if (isExist)
        {
            _contextDataRepository.Exceptions.Should().BeEmpty();
        }
    }
    
    protected void ThrowExceptionAssertion(string exceptionText)
    {
        var isExist = _contextDataRepository.Exceptions is not null;
        if (isExist)
        {
            _contextDataRepository.Exceptions.Should().NotBeEmpty();
            var relatedException = _contextDataRepository.Exceptions!.FirstOrDefault(s => s.Message == exceptionText);
            var allExceptions = string.Join(",",_contextDataRepository.Exceptions!.Select(s => s.Message).ToList());
            relatedException.Should().NotBeNull($"and all exceptions are: {allExceptions}");
        }
        else
        {
            throw new AssertionFailedException("exception does not created");
        }
    }
    
}
