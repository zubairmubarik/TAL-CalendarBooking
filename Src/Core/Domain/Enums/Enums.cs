namespace Domain.Enums
{
    #region Business Error Enums

    /// <summary>
    /// Defined Enum for Busienss Log Levels
    /// </summary>
    public enum BusinessLogLevel
    {
        Trace = 0,
        Debug = 1,
        Information = 2,
        Warning = 3, Error = 4,
        Critical = 5,
        None = 6
    }

    /// <summary>
    /// Defined Enum for Busienss Error Codes
    /// https://developer.mozilla.org/en-US/docs/Web/HTTP/Status#information_responses
    /// </summary>
    public enum ResponseCode
    {
        Ok = 200, //200+ Successful
        Created = 201, //The request has succeeded and a new resource has been created as a result. This is typically the response sent after POST requests, or some PUT requests.
                       // 300 Redirect
        BadRequest = 400, // 400+ Client
        Unauthorized = 401,
        NotFound = 404,

        InternalServerError = 500, // 500+ Server

        BusinessError = 600,
        LessThanZero = 601,
        EmptyObject = 602,
        FailedToProcess = 603,
        OutOfRange = 604,
        ValidationError = 605
    }

    #endregion
}
