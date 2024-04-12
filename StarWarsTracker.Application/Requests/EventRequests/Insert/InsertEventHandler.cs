using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.Insert
{
    internal class InsertEventHandler : DataRequestHandler<InsertEventRequest>
    {
        public InsertEventHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        public override async Task ExecuteRequestAsync(InsertEventRequest request)
        {
            _logger.AddInfo("Executing Request", request.GetType().Name);

            _logger.AddDebug("Request Body", request);

            var nameExistsDTO = await _dataAccess.FetchAsync(new IsEventNameExisting(request.Name));

            _logger.AddDebug("IsEventNameExisting DTO", nameExistsDTO);

            if (nameExistsDTO!.IsExistingInCanonType(request.CanonType))
            {
                _logger.AddInfo("Event Already Exists In CanonType", nameof(request.CanonType));

                throw new AlreadyExistsException(nameof(Event), (request.CanonType, nameof(request.CanonType)), (request.Name, nameof(request.Name)));
            }

            _logger.AddTrace("Inserting Event");

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertEvent(Guid.NewGuid(), request.Name, request.Description, (int)request.CanonType));

            _logger.AddInfo("Rows Affected When Inserting Event", rowsAffected);

            if (rowsAffected <= 0)
            {
                _logger.IncreaseLevel(LogLevel.Critical, "Failed To Insert Event");

                throw new OperationFailedException();
            }
        }
    }
}
