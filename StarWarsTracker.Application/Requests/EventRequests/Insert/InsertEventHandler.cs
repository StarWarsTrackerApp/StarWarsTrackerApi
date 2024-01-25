using StarWarsTracker.Application.BaseObjects.BaseHandlers;
using StarWarsTracker.Persistence.DataRequestObjects.EventRequests;

namespace StarWarsTracker.Application.Requests.EventRequests.Insert
{
    internal class InsertEventHandler : DataRequestHandler<InsertEventRequest>
    {
        public InsertEventHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task ExecuteRequestAsync(InsertEventRequest request)
        {
            var guid = Guid.NewGuid();

            var nameExistsDTO = await _dataAccess.FetchAsync(new IsEventNameExisting(request.Name));

            if (IsNameAlreadyExisting(request.CanonType, nameExistsDTO!))
            {
                throw new AlreadyExistsException(nameof(Event),
                    (request.CanonType, nameof(request.CanonType)),
                    (request.Name, nameof(request.Name))
                );
            }

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertEvent(guid, request.Name, request.Description, (int)request.CanonType));

            if (rowsAffected <= 0)
            {
                throw new OperationFailedException();
            }
        }

        private bool IsNameAlreadyExisting(CanonType canonType, IsEventNameExisting_DTO isNameExisting)
        {
            return canonType switch
            {
                CanonType.StrictlyCanon => isNameExisting.NameExistsInStrictlyCanon || isNameExisting.NameExistsInCanonAndLegends,
                CanonType.StrictlyLegends => isNameExisting.NameExistsInStrictlyLegends || isNameExisting.NameExistsInCanonAndLegends,
                CanonType.CanonAndLegends => isNameExisting.NameExistsInCanonAndLegends || isNameExisting.NameExistsInStrictlyCanon || isNameExisting.NameExistsInStrictlyLegends,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
