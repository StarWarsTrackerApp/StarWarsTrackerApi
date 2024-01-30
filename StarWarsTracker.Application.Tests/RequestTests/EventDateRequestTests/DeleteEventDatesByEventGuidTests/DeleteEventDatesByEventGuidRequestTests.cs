using StarWarsTracker.Application.Requests.EventDateRequests.Delete;

namespace StarWarsTracker.Application.Tests.RequestTests.EventDateRequestTests.DeleteEventDatesByEventGuidTests
{
    public class DeleteEventDatesByEventGuidRequestTests
    {
        [Fact]
        public void DeleteEventDatesByEventGuidRequest_Given_EventGuidNotSet_IsValid_ShouldReturn_False()
        {
            var request = new DeleteEventDatesByEventGuidRequest();

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteEventDatesByEventGuidRequest_Given_EventGuidIsEmpty_IsValid_ShouldReturn_False()
        {
            var request = new DeleteEventDatesByEventGuidRequest(Guid.Empty);

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteEventDatesByEventGuidRequest_Given_EventGuidIsValidGuid_IsValid_ShouldReturn_True()
        {
            var request = new DeleteEventDatesByEventGuidRequest(Guid.NewGuid());

            Assert.True(request.IsValid(out _));
        }
    }
}
