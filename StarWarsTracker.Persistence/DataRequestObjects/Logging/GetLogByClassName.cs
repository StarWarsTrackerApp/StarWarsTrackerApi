namespace StarWarsTracker.Persistence.DataRequestObjects.Logging
{
    public class GetLogByClassName : IDataFetch<Log_DTO>
    {
        #region Constructor

        public GetLogByClassName(string className)
        {
            ClassName = className;
        }

        #endregion

        #region Public Properties / SQL Parameters

        public string ClassName { get; set; }

        #endregion

        #region Public IDataFetch Methods
        
        public object? GetParameters() => this;

        public string GetSql() => $"SELECT * FROM {TableName.Log} WITH(NOLOCK) WHERE ClassName = @ClassName";
        
        #endregion
    }
}
