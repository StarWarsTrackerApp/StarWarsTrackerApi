using StarWarsTracker.Application.BaseObjects.BaseHandlers;
using StarWarsTracker.Application.Requests.EventRequests.GetByGuid;
using StarWarsTracker.Persistence.DataRequestObjects.EventDateRequests;

namespace StarWarsTracker.Application.Requests.EventDateRequests.Insert
{
    internal class InsertEventTimeFrameHandler : DataOrchestratorRequestHandler<InsertEventTimeFrameRequest>
    {
        public InsertEventTimeFrameHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task ExecuteRequestAsync(InsertEventTimeFrameRequest request)
        {
            var eventByGuid = await _orchestrator.GetRequestResponseAsync<GetEventByGuidRequest, GetEventByGuidResponse>(new GetEventByGuidRequest(request.EventGuid));

            if(eventByGuid.EventTimeFrame != null)
            {
                throw new AlreadyExistsException(nameof(EventTimeFrame), (request.EventGuid, nameof(request.EventGuid)));
            }

            foreach (var date in request.EventDates)
            {
                var rowsUpdated = await _dataAccess.ExecuteAsync(new InsertEventDate(request.EventGuid, (int)date.EventDateType, date.YearsSinceBattleOfYavin, date.Sequence));
            
                if (rowsUpdated <= 0)
                {
                    throw new OperationFailedException();
                }
            }
        }
    }
}
