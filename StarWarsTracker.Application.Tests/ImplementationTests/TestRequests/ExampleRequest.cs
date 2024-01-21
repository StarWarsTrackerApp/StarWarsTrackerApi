using StarWarsTracker.Application.Abstraction;

namespace StarWarsTracker.Application.Tests.ImplementationTests.TestRequests
{
    internal class ExampleRequest : IRequest { }

    internal class ExampleRequestHandler : IRequestHandler<ExampleRequest>
    {
        public Task ExecuteRequestAsync(ExampleRequest request) => Task.CompletedTask;
    }
}
