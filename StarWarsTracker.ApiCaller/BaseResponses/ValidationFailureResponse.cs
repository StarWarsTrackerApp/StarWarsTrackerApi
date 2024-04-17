using StarWarsTracker.ApiCaller.Implementation;
using System.Net;

namespace StarWarsTracker.ApiCaller.BaseResponses
{
    public class ValidationFailureResponse : ApiResponse
    {
        public ValidationFailureResponse(IEnumerable<string> validationFailureReasons) : base(HttpStatusCode.BadRequest, validationFailureReasons)
        {
            _validationFailureReasons = validationFailureReasons;
        }

        private readonly IEnumerable<string> _validationFailureReasons;

        public IEnumerable<string> ValidationFailureReasons => _validationFailureReasons;
    }
}
