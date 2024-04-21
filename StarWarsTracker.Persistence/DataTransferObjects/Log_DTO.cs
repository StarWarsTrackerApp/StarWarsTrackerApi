namespace StarWarsTracker.Persistence.DataTransferObjects
{
    /// <summary>
    /// DTO representing the Log table exactly.
    /// </summary>
    public class Log_DTO
    {
        public int Id { get; set; }

        public int LogLevelId { get; set; }

        public string Message { get; set; } = string.Empty;

        public string ClassName { get; set; } = string.Empty;

        public string MethodName { get; set; } = string.Empty;

        public string? StackTrace { get; set; }
    }
}
