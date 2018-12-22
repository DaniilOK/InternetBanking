using System.Net;

namespace InternetBanking.ErrorHandling
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        // internal error code
        public int ErrorCode { get; set; }

        // message with description of the error
        public string Message { get; set; }
    }
}
