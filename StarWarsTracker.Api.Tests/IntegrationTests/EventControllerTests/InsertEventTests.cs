//using StarWarsTracker.Application.Requests.EventRequests.Delete;
//using StarWarsTracker.Application.Requests.EventRequests.Insert;
//using StarWarsTracker.Domain.Constants;
//using StarWarsTracker.Domain.Enums;
//using StarWarsTracker.Domain.Exceptions;
//using StarWarsTracker.Domain.Validation;
//using StarWarsTracker.Tests.Shared.Helpers;

//namespace StarWarsTracker.Api.Tests.IntegrationTests.EventControllerTests
//{
//    public class InsertEventTests : ControllerTest<EventController>
//    {
//        #region Bad Request Tests

//        [Theory]
//        [InlineData("")]
//        [InlineData("  ")]
//        [InlineData(null)]        
//        public async Task InsertEvent_Given_EventName_IsNullOrEmpty_ShouldThrow_ValidationFailedException_WithMessage_NameRequired(string name)
//        {
//            var insertRequest = new InsertEventRequest(name, "Description", CanonType.CanonAndLegends);

//            var expectedMessage = ValidationFailureMessage.RequiredField(nameof(insertRequest.Name));

//            var exception = await Record.ExceptionAsync(async () => await _controller.InsertEvent(insertRequest)) as ValidationFailureException;

//            Assert.NotNull(exception);

//            Assert.Equal(expectedMessage, exception.GetResponseBody().ValidationFailureReasons.Single());
//        }

//        [Theory]
//        [InlineData(MaxLength.EventName + 1)]
//        [InlineData(MaxLength.EventName + 5)]
//        [InlineData(MaxLength.EventName + 10)]
//        public async Task InsertEvent_Given_EventName_ExceedsMaxLength_ShouldThrow_ValidationFailedException_WithMessage_NameExceedingMaxLength(int nameLength)
//        {
//            var insertRequest = new InsertEventRequest(TestString.Random(nameLength), "Description", CanonType.CanonAndLegends);

//            var expectedMessage = ValidationFailureMessage.StringExceedingMaxLength(insertRequest.Name, nameof(insertRequest.Name), MaxLength.EventName);

//            var exception = await Record.ExceptionAsync(async () => await _controller.InsertEvent(insertRequest)) as ValidationFailureException;

//            Assert.NotNull(exception);

//            Assert.Equal(expectedMessage, exception.GetResponseBody().ValidationFailureReasons.Single());
//        }

//        [Theory]
//        [InlineData("")]
//        [InlineData("  ")]
//        [InlineData(null)]
//        public async Task InsertEvent_Given_EventDescription_IsNullOrEmpty_ShouldThrow_ValidationFailedException_WithMessage_DescriptionRequired(string description)
//        {
//            var insertRequest = new InsertEventRequest(TestString.Random(), description, CanonType.CanonAndLegends);

//            var expectedMessage = ValidationFailureMessage.RequiredField(nameof(insertRequest.Description));

//            var exception = await Record.ExceptionAsync(async () => await _controller.InsertEvent(insertRequest)) as ValidationFailureException;

//            Assert.NotNull(exception);

//            Assert.Equal(expectedMessage, exception.GetResponseBody().ValidationFailureReasons.Single());
//        }

//        [Fact]
//        public async Task InsertEvent_Given_EventCanonType_IsNotValid_ShouldThrow_ValidationFailedException_WithMessage_CanonTypeInvalidValue()
//        {
//            var insertRequest = new InsertEventRequest(TestString.Random(), TestString.Random(), 0);

//            var expectedMessage = ValidationFailureMessage.InvalidValue(insertRequest.CanonType, nameof(insertRequest.CanonType));

//            var exception = await Record.ExceptionAsync(async () => await _controller.InsertEvent(insertRequest)) as ValidationFailureException;

//            Assert.NotNull(exception);

//            Assert.Equal(expectedMessage, exception.GetResponseBody().ValidationFailureReasons.Single());
//        }

//        #endregion

//        #region Already Exists Tests

//        [Theory]
//        [InlineData(CanonType.StrictlyCanon, CanonType.StrictlyCanon)]
//        [InlineData(CanonType.StrictlyCanon, CanonType.CanonAndLegends)]
//        [InlineData(CanonType.StrictlyLegends, CanonType.StrictlyLegends)]
//        [InlineData(CanonType.StrictlyLegends, CanonType.CanonAndLegends)]
//        [InlineData(CanonType.CanonAndLegends, CanonType.CanonAndLegends)]
//        [InlineData(CanonType.CanonAndLegends, CanonType.StrictlyCanon)]
//        [InlineData(CanonType.CanonAndLegends, CanonType.StrictlyLegends)]
//        public async Task InsertEvent_Given_EventNameAlreadyExistsInCanonType_ShouldThrow_AlreadyExistsException(CanonType canonTypeAlreadyExisting, CanonType canonTypeToInsert)
//        {
//            var nameToInsert = TestString.Random();
            
//            var existingEvent = await TestEvent.InsertAndFetchEventAsync(name: nameToInsert, canonType: canonTypeAlreadyExisting);

//            var request = new InsertEventRequest(nameToInsert, TestString.Random(), canonTypeToInsert);

//            var exception = await Record.ExceptionAsync(async () => await _controller.InsertEvent(request));

//            await _controller.DeleteEvent(new DeleteEventByGuidRequest(existingEvent.Guid));

//            Assert.NotNull(exception);
//            Assert.IsType<AlreadyExistsException>(exception);
//        }

//        #endregion

//        #region Insert Event Successfully Test

//        [Fact]
//        public async Task InsertEvent_Given_TaskCompletedSuccessfully_ShouldInsertEvent()
//        {
//            var request = new InsertEventRequest(TestString.Random(), TestString.Random(), CanonType.CanonAndLegends);

//            var doesNotExistExceptionBeforeInserting = await Record.ExceptionAsync(async () => await _controller.GetEventByNameAndCanonType(new(request.Name, request.CanonType))) as DoesNotExistException;

//            var insertTask = _controller.InsertEvent(request);

//            await insertTask;

//            var eventInserted = await _controller.GetEventByNameAndCanonType(new(request.Name, request.CanonType));

//            await _controller.DeleteEvent(new DeleteEventByGuidRequest(eventInserted.Event.Guid));

//            // Assert that the InsertEvent was completed successfully
//            Assert.True(insertTask.IsCompletedSuccessfully);

//            // Assert that the attempt to get the event before inserting threw a DoesNotExistException
//            Assert.NotNull(doesNotExistExceptionBeforeInserting);

//            // Assert that the EventInserted returned was not null and had correct values
//            Assert.NotNull(eventInserted);
//            Assert.Equal(request.Name, eventInserted.Event.Name);
//            Assert.Equal(request.Description, eventInserted.Event.Description);
//            Assert.Equal(request.CanonType, eventInserted.Event.CanonType);
//        }

//        #endregion
//    }
//}
