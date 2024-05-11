using StarWarsTracker.Logging.Abstraction;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.Insert
{
    internal class InsertEventHandler : DataRequestHandler<InsertEventRequest>
    {
        public InsertEventHandler(IDataAccess dataAccess, IClassLoggerFactory loggerFactory) : base(dataAccess, loggerFactory) { }

        internal protected override async Task<IResponse> HandleRequestAsync(InsertEventRequest request)
        {
            var nameExistsDTO = await _dataAccess.FetchAsync(new IsEventNameExisting(request.Name));

            if (nameExistsDTO!.IsExistingInCanonType(request.CanonType))
            {
                return Response.AlreadyExists(nameof(Event), (request.CanonType, nameof(request.CanonType)), (request.Name, nameof(request.Name)));
            }

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertEvent(Guid.NewGuid(), request.Name, request.Description, (int)request.CanonType));

            if (rowsAffected <= 0)
            {
                _logger.IncreaseLevel(LogLevel.Critical, "Failed To Insert Event");

                return Response.Error();
            }

            return Response.Success();
        }
    }
}
