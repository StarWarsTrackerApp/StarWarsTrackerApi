namespace StarWarsTracker.Persistence.DataRequestObjects.Logging
{
    public class GetLogByClassName : IDataFetch<Log_DTO>
    {
        public string ClassName { get; set; }

        public GetLogByClassName(string className)
        {
            ClassName = className;
        }

        public object? GetParameters() => this;

        public string GetSql() => $"SELECT * FROM {TableName.Log} WITH(NOLOCK) WHERE ClassName = @ClassName";
    }
}
