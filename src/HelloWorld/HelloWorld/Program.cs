using OCore.Services;

await OCore.Setup.Developer.LetsGo("Hello World");

[Service("HelloWorld")]
public interface IHelloWorld : IService
{
    Task<string> Greet(string somebody);
}

public class HelloWorldService : Service, IHelloWorld
{
    int i = 0;
    public Task<string> Greet(string somebody)
    {
        i++;
        return Task.FromResult($"Hello, {somebody}! {i}");
    }
}
