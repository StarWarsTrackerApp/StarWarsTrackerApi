using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StarWarsTracker.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NotFoundResponse : IResponse
    {
        #region Constructor

        public NotFoundResponse(string nameOfObject, params (object? Value, string NameOfField)[] valuesSearchedBy)
        {
            NameOfObjectNotExisting = nameOfObject;
            ValuesSearchedBy = valuesSearchedBy.Any() 
                             ? valuesSearchedBy.Select(_ => new KeyValuePair<string, object?>(_.NameOfField, _.Value)) 
                             : Enumerable.Empty<KeyValuePair<string, object?>>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the object that does not exist.
        /// </summary>
        public string NameOfObjectNotExisting { get; set; }

        /// <summary>
        /// The Values that were searched by (NameOfField, Value)
        /// </summary>
        public IEnumerable<KeyValuePair<string, object?>> ValuesSearchedBy { get; set; }

        #endregion

        #region Public IResponse Methods

        public object? GetBody() => this;

        public int GetStatusCode() => (int)HttpStatusCode.NotFound;

        #endregion
    }
}
