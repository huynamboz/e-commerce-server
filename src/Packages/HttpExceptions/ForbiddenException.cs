using System.Net;

namespace e_commerce_server.src.Packages.HttpExceptions
{
    public class ForbiddenException : HttpException
    {
        public ForbiddenException(string message = "You don't have permission to access this resource") : base(HttpStatusCode.Forbidden, ErrorEnum.FORBIDDEN, message) { }
    }
}
