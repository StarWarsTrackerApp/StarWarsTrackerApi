using StarWarsTracker.Application.Requests.EventRequests.Delete;

namespace StarWarsTracker.Application.Tests.RequestTests.EventRequestTests.DeleteEventTests
{
    public class DeleteEventByGuidRequestTests
    {
        [Fact]
        public void DeleteEventByGuidRequest_Given_EventGuidNotSet_IsValid_ShouldReturn_False()
        {
            var request = new DeleteEventByGuidRequest();

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteEventByGuidRequest_Given_EventGuidIsEmpty_IsValid_ShouldReturn_False()
        {
            var request = new DeleteEventByGuidRequest(Guid.Empty);

            Assert.False(request.IsValid(out _));
        }

        [Fact]
        public void DeleteEventByGuidRequest_Given_EventGuidIsValidGuid_IsValid_ShouldReturn_True()
        {
            var request = new DeleteEventByGuidRequest(Guid.NewGuid());

            Assert.True(request.IsValid(out _));
        }
    }
}
