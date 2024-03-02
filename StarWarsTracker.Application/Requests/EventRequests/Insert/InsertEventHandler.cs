using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.Insert
{
    internal class InsertEventHandler : DataRequestHandler<InsertEventRequest>
    {
        public InsertEventHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task ExecuteRequestAsync(InsertEventRequest request)
        {
            var nameExistsDTO = await _dataAccess.FetchAsync(new IsEventNameExisting(request.Name));

            if (nameExistsDTO!.IsExistingInCanonType(request.CanonType))
            {
                throw new AlreadyExistsException(nameof(Event), (request.CanonType, nameof(request.CanonType)), (request.Name, nameof(request.Name)));
            }

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertEvent(Guid.NewGuid(), request.Name, request.Description, (int)request.CanonType));

            if (rowsAffected <= 0)
            {
                throw new OperationFailedException();
            }
        }
    }
}
