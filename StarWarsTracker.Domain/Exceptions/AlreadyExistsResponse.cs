namespace StarWarsTracker.Application.BaseObjects.ExceptionResponses
{
    /// <summary>
    /// This class represents the response the API would return when an Exception occurs due to an object already existing.
    /// </summary>
    public class AlreadyExistsResponse
    {
        #region Constructor

        public AlreadyExistsResponse(string nameOfObject, params (object Value, string NameOfField)[] possibleConflicts)
        {
            NameOfObjectAlreadyExisting = nameOfObject;

            PossibleConflicts = possibleConflicts.Any() ? possibleConflicts.Select( _ => new { Field = _.NameOfField, _.Value }) : Enumerable.Empty<object>();
        }

        #endregion

        #region Public Propertieds

        /// <summary>
        /// The name of the object that is already existing.
        /// </summary>
        public string NameOfObjectAlreadyExisting { get; set; }

        /// <summary>
        /// The Fields/Values that may have caused the Conflict.
        /// </summary>
        public IEnumerable<object> PossibleConflicts { get; set; }
        
        #endregion
    }
}
