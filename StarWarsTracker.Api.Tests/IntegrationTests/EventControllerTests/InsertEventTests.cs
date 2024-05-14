using StarWarsTracker.Api.Tests.TestHelpers;
using StarWarsTracker.Application.BaseObjects.ExceptionResponses;
using StarWarsTracker.Application.Requests.EventRequests.Delete;
using StarWarsTracker.Application.Requests.EventRequests.Insert;
using StarWarsTracker.Domain.Constants;
using StarWarsTracker.Domain.Enums;
using StarWarsTracker.Domain.Models;
using StarWarsTracker.Domain.Validation;
using StarWarsTracker.Tests.Shared.Helpers;

namespace StarWarsTracker.Api.Tests.IntegrationTests.EventControllerTests
{
    public class InsertEventTests : ControllerTest<EventController>
    {
        #region Bad Request Tests

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task InsertEvent_Given_EventName_IsNullOrEmpty_ShouldReturn_BadRequestResponse_WithMessage_NameRequired(string name)
        {
            var insertRequest = new InsertEventRequest(name, "Description", CanonType.CanonAndLegends);

            var expectedMessage = ValidationFailureMessage.RequiredField(nameof(insertRequest.Name));

            var result = await _controller.InsertEvent(insertRequest);

            var responseBody = result.GetResponseBody<ValidationFailureResponse>();

            Assert.Equal(StatusCodes.Status400BadRequest, result.GetStatusCode());

            Assert.NotNull(responseBody);

            Assert.Equal(expectedMessage, responseBody.ValidationFailureReasons.Single());
        }

        [Theory]
        [InlineData(MaxLength.EventName + 1)]
        [InlineData(MaxLength.EventName + 5)]
        [InlineData(MaxLength.EventName + 10)]
        public async Task InsertEvent_Given_EventName_ExceedsMaxLength_ShouldReturn_BadRequestResponse_WithMessage_NameExceedingMaxLength(int nameLength)
        {
            var insertRequest = new InsertEventRequest(TestString.Random(nameLength), "Description", CanonType.CanonAndLegends);

            var expectedMessage = ValidationFailureMessage.StringExceedingMaxLength(insertRequest.Name, nameof(insertRequest.Name), MaxLength.EventName);

            var result = await _controller.InsertEvent(insertRequest);

            var responseBody = result.GetResponseBody<ValidationFailureResponse>();

            Assert.Equal(StatusCodes.Status400BadRequest, result.GetStatusCode());

            Assert.NotNull(responseBody);

            Assert.Equal(expectedMessage, responseBody.ValidationFailureReasons.Single());
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task InsertEvent_Given_EventDescription_IsNullOrEmpty_ShouldReturn_BadRequestResponse_WithMessage_DescriptionRequired(string description)
        {
            var insertRequest = new InsertEventRequest(TestString.Random(), description, CanonType.CanonAndLegends);

            var expectedMessage = ValidationFailureMessage.RequiredField(nameof(insertRequest.Description));

            var result = await _controller.InsertEvent(insertRequest);

            var responseBody = result.GetResponseBody<ValidationFailureResponse>();

            Assert.Equal(StatusCodes.Status400BadRequest, result.GetStatusCode());

            Assert.NotNull(responseBody);

            Assert.Equal(expectedMessage, responseBody.ValidationFailureReasons.Single());
        }

        [Fact]
        public async Task InsertEvent_Given_EventCanonType_IsNotValid_ShouldReturn_BadRequestResponse_WithMessage_CanonTypeInvalidValue()
        {
            var insertRequest = new InsertEventRequest(TestString.Random(), TestString.Random(), 0);

            var expectedMessage = ValidationFailureMessage.InvalidValue(insertRequest.CanonType, nameof(insertRequest.CanonType));

            var result = await _controller.InsertEvent(insertRequest);

            var responseBody = result.GetResponseBody<ValidationFailureResponse>();

            Assert.Equal(StatusCodes.Status400BadRequest, result.GetStatusCode());

            Assert.NotNull(responseBody);

            Assert.Equal(expectedMessage, responseBody.ValidationFailureReasons.Single());
        }

        #endregion

        #region Already Exists Tests

        [Theory]
        [InlineData(CanonType.StrictlyCanon, CanonType.StrictlyCanon)]
        [InlineData(CanonType.StrictlyCanon, CanonType.CanonAndLegends)]
        [InlineData(CanonType.StrictlyLegends, CanonType.StrictlyLegends)]
        [InlineData(CanonType.StrictlyLegends, CanonType.CanonAndLegends)]
        [InlineData(CanonType.CanonAndLegends, CanonType.CanonAndLegends)]
        [InlineData(CanonType.CanonAndLegends, CanonType.StrictlyCanon)]
        [InlineData(CanonType.CanonAndLegends, CanonType.StrictlyLegends)]
        public async Task InsertEvent_Given_EventNameAlreadyExistsInCanonType_ShouldReturn_ConflictResponse(CanonType canonTypeAlreadyExisting, CanonType canonTypeToInsert)
        {
            var nameToInsert = TestString.Random();

            var existingEvent = await TestEvent.InsertAndFetchEventAsync(name: nameToInsert, canonType: canonTypeAlreadyExisting);

            var request = new InsertEventRequest(nameToInsert, TestString.Random(), canonTypeToInsert);

            var result = await _controller.InsertEvent(request);

            var responseBody = result.GetResponseBody<AlreadyExistsResponse>();

            await _controller.DeleteEvent(new DeleteEventByGuidRequest(existingEvent.Guid));

            Assert.Equal(StatusCodes.Status409Conflict, result.GetStatusCode());

            Assert.NotNull(responseBody);
        }

        #endregion

        #region Insert Event Successfully Test

        [Fact]
        public async Task InsertEvent_Given_EventInserted_ShouldReturn_SuccessResponse()
        {
            var request = new InsertEventRequest(TestString.Random(), TestString.Random(), CanonType.CanonAndLegends);

            var fetchResultBeforeInserting = await _controller.GetEventByNameAndCanonType(new(request.Name, request.CanonType));

            var result = await _controller.InsertEvent(request);

            var fetchResultAfterInserting = await _controller.GetEventByNameAndCanonType(new(request.Name, request.CanonType));

            var eventInserted = fetchResultAfterInserting.GetResponseBody<Event>();


            Assert.Equal(StatusCodes.Status200OK, result.GetStatusCode());
            Assert.NotNull(eventInserted);
            
            await _controller.DeleteEvent(new DeleteEventByGuidRequest(eventInserted.Guid));

            Assert.Equal(StatusCodes.Status404NotFound, fetchResultBeforeInserting.GetStatusCode());

            Assert.Equal(StatusCodes.Status200OK, fetchResultAfterInserting.GetStatusCode());

            // Assert that the EventInserted returned was not null and had correct values
            Assert.NotNull(fetchResultAfterInserting);
            Assert.Equal(request.Name, eventInserted.Name);
            Assert.Equal(request.Description, eventInserted.Description);
            Assert.Equal(request.CanonType, eventInserted.CanonType);
        }

        #endregion
    }
}
