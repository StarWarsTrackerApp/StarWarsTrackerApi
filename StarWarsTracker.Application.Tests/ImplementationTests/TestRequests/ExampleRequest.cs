using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Application.BaseObjects.BaseHandlers;
using StarWarsTracker.Domain.Logging;

namespace StarWarsTracker.Application.Tests.ImplementationTests.TestRequests
{
    internal class ExampleRequest : IRequest { }

    internal class ExampleRequestHandler : BaseRequestHandler<ExampleRequest>
    {
        public ExampleRequestHandler(ILogMessage logMessage) : base(logMessage)
        {
        }

        public override Task ExecuteRequestAsync(ExampleRequest request) => Task.CompletedTask;
    }
}
