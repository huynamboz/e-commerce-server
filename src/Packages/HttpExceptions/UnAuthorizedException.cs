using e_commerce_server.Src.Packages.HttpException;
using System.Net;

namespace e_commerce_server.Src.Packages.HttpException
{
    public class UnAuthorizedException : HttpException
    {
        public UnAuthorizedException(string message = "Invalid access token") : base(HttpStatusCode.Unauthorized, ErrorEnum.UNAUTHORIZED, message) { }
    }
}
