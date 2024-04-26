namespace StarWarsTracker.Domain.Exceptions
{
    public abstract class CustomException : Exception
    {
        public abstract int GetStatusCode();

        public abstract object GetResponseBody();

        public abstract string GetLogLevelConfigKey();
    }
}
