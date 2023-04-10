using System.Net;

namespace e_commerce_server.src.Packages.HttpExceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Response { get; set; }

        public HttpException(HttpStatusCode statusCode, string code, string message) : base(message)
        {
            StatusCode = statusCode;

            Response = new
            {
                message,
                status = statusCode,
                code
            };
        }
    }
}
