using System.Net;

namespace Pharmacy.WebApi.Common.Exceptions
{
    public class StatusCodeException : Exception
    {

        public HttpStatusCode StatusCode { get; set; }
        public StatusCodeException(HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            StatusCode = httpStatusCode;
        }
    }
}
