using Microsoft.Extensions.Logging;
using OCore.Events;
using OCore.Services;
using Orleans;

await OCore.Setup.Developer.LetsGo("Events");

[Event("Test")]
[GenerateSerializer]
public class TestEvent
{
    [Id(0)]
    public string? Message { get; init; }
}

[Service("FireEvent")]
public interface IFireEventService : IService
{
    Task FireEvent(string message);
}

public class FireEventService : Service, IFireEventService
{
    public Task FireEvent(string message)
    {
        GrainFactory.GetEventAggregator().Raise(new TestEvent
        {
            Message = message
        });
        return Task.CompletedTask;
    }
}

[Handler("Test")]
public class TestEventHandler : Handler<TestEvent>
{
    private readonly ILogger _logger;

    public TestEventHandler(ILogger<TestEventHandler> logger)
    {
        _logger = logger;
    }
    protected override Task HandleEvent(TestEvent @event)
    {
        _logger.LogInformation(@event.Message);
        return base.HandleEvent(@event);
    }
}