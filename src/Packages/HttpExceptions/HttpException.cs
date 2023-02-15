using System.Net;
using System.Runtime.Serialization;

namespace e_commerce_server.src.Packages.HttpException
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
