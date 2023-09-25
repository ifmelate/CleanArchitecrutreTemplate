using System.Diagnostics.CodeAnalysis;
using MassTransit;
using ProjectName.ServiceName.Domain.Events;

namespace ProjectName.ServiceName.Infrastructure.Consumers;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class TestConsumer : IConsumer<TestEvent>
{
    public async Task Consume(ConsumeContext<TestEvent> context)
    {
        // Process the received message
        Console.WriteLine($"Received message: {context.Message.TestMessage}");
        await context.ConsumeCompleted;
    }
}
