using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Persistence.DataRequestObjects.Logging;
using System.Data.SqlClient;

namespace StarWarsTracker.Persistence.Tests.DataRequestTests.LoggingRequestTests
{
    public class InsertLogTests : DataRequestTest
    {
        [Fact]
        public async Task InsertLog_Given_LogInserted_ShouldReturn_OneRowAffected()
        {
            var request = new InsertLog((int)LogLevel.Debug, "Message", className: TestString.Random(), methodName: "Test InsertLog - One Row Affected", stackTrace: "Stack Trace");

            var rowsAffected = await _dataAccess.ExecuteAsync(request);

            Assert.Equal(1, rowsAffected);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public async Task InsertLog_Given_InvalidLogLevel_ShouldThrow_SqlException(int invalidLogLevel)
        {
            var request = new InsertLog(invalidLogLevel, "Message", className: TestString.Random(), methodName: "Test InsertLog - Invalid Log Level", stackTrace: "Stack Trace");

            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(request));

            Assert.IsType<SqlException>(exception);
        }
    }
}
