using Moq;
using StarWarsTracker.Application.Abstraction;
using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.Abstraction;

namespace StarWarsTracker.Application.Tests.RequestTests
{
    public abstract class HandlerTest
    {
        protected static readonly Mock<IClassLoggerFactory> _mockLoggerFactory = new();

        protected readonly Mock<IClassLogger> _mockLogger = new();

        protected readonly Mock<IDataAccess> _mockDataAccess = new();
        
        protected readonly Mock<IOrchestrator> _mockOrchestrator = new();

        public HandlerTest()
        {
            _mockLoggerFactory.Setup(_ => _.GetLoggerFor(It.IsAny<It.IsAnyType>())).Returns(() => _mockLogger.Object);
        }

        #region Helpers for setting up Mock calls to database with IDataAccess

        protected void SetupMockFetchAsync<TRequest, TResponse>(TResponse? response) where TRequest : IDataFetch<TResponse> =>
            _mockDataAccess.Setup(_ => _.FetchAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));

        protected void SetupMockFetchListAsync<TRequest, TResponse>(IEnumerable<TResponse> response) where TRequest : IDataFetch<TResponse> =>
            _mockDataAccess.Setup(_ => _.FetchListAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));

        protected void SetupMockExecuteAsync<TRequest>(int response) where TRequest : IDataExecute =>
            _mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));

        #endregion

        #region Helpers for setting up Mock calls to IOrchestrator

        protected void SetupMockGetRequestResponseAsync<TRequest, TResponse>(TResponse? response) where TRequest : IRequestResponse<TResponse> =>
            _mockOrchestrator.Setup(_ => _.GetRequestResponseAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response!));

        #endregion
    }
}
