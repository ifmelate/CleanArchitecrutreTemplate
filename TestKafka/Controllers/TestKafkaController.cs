using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProjectName.ServiceName.Domain.Events;

namespace TestKafka.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestKafkaController : ControllerBase
{

    [HttpGet]
    public async Task<IResult> TestKafka([FromServices] ITopicProducer<string, TestEvent> topicProducer)
    { 
        await topicProducer.Produce("testKey", new TestEvent ("my kafka received a message!"), HttpContext.RequestAborted);
        return Results.NoContent();
    }
    
}
