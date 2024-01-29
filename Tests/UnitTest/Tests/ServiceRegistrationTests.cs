using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Tests;

public class ServiceRegistrationTests
{
    [Fact]
    public Task AddInfrastructureTest()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddInfrastructure();

        return Task.CompletedTask;
    }
}