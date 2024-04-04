using StarWarsTracker.Domain.Logging;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.Insert
{
    internal class InsertEventHandler : DataRequestHandler<InsertEventRequest>
    {
        public InsertEventHandler(IDataAccess dataAccess, ILogMessage logMessage) : base(dataAccess, logMessage) { }

        public override async Task ExecuteRequestAsync(InsertEventRequest request)
        {
            _logMessage.AddInfo(this, "Executing Request", request.GetType().Name);

            _logMessage.AddDebug(this, "Request Body", request);

            var nameExistsDTO = await _dataAccess.FetchAsync(new IsEventNameExisting(request.Name));

            _logMessage.AddDebug(this, "IsEventNameExisting DTO", nameExistsDTO);

            if (nameExistsDTO!.IsExistingInCanonType(request.CanonType))
            {
                _logMessage.AddInfo(this, "Event Already Exists In CanonType");

                throw new AlreadyExistsException(nameof(Event), (request.CanonType, nameof(request.CanonType)), (request.Name, nameof(request.Name)));
            }

            _logMessage.AddTrace(this, "Inserting Event");

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertEvent(Guid.NewGuid(), request.Name, request.Description, (int)request.CanonType));

            _logMessage.AddInfo(this, "Rows Affected When Inserting Event", rowsAffected);

            if (rowsAffected <= 0)
            {
                _logMessage.IncreaseLevel(LogLevel.Critical, this, "Failed To Insert Event");

                throw new OperationFailedException();
            }
        }
    }
}
