using System.Net;

namespace e_commerce_server.src.Packages.HttpExceptions
{
    public class UnAuthorizedException : HttpException
    {
        public UnAuthorizedException(string message = "Invalid access token") : base(HttpStatusCode.Unauthorized, ErrorEnum.UNAUTHORIZED, message) { }
    }
}
