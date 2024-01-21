using StarWarsTracker.Domain.Enums;

namespace StarWarsTracker.Persistence.DataRequestObjects.EventRequests
{
    public class IsEventNameExisting : IDataFetch<IsEventNameExisting_DTO>
    {
        public IsEventNameExisting(string eventName) => EventName = eventName;

        public string EventName { get; set; }

        public object? GetParameters() => this;

        public string GetSql() =>
        $@"
            SELECT
                CASE WHEN EXISTS(SELECT 1 FROM {TableName.Event} WHERE Name = @EventName AND CanonTypeId = {(int)CanonType.StrictlyCanon} )
                    THEN 1 ELSE 0 END AS NameExistsInStrictlyCanon,
                CASE WHEN EXISTS(SELECT 1 FROM {TableName.Event} WHERE Name = @EventName AND CanonTypeId = {(int)CanonType.StrictlyLegends} )
                    THEN 1 ELSE 0 END AS NameExistsInStrictlyLegends,
                CASE WHEN EXISTS(SELECT 1 FROM {TableName.Event} WHERE Name = @EventName AND CanonTypeId = {(int)CanonType.CanonAndLegends} )
                    THEN 1 ELSE 0 END AS NameExistsInCanonAndLegends
        ";
    }
}
