using System.Net;

namespace e_commerce_server.src.Packages.HttpExceptions
{
    public class InternalException : HttpException
    {
        public InternalException(string message = "Internal server error") : base(HttpStatusCode.InternalServerError, ErrorEnum.INTERNAL, message) { }
    }
}
