using e_commerce_server.Src.Packages.HttpException;
using System.Net;

namespace e_commerce_server.Src.Packages.HttpException
{
    public class InternalException : HttpException
    {
        public InternalException(string message = "Internal server error") : base(HttpStatusCode.InternalServerError, ErrorEnum.INTERNAL, message) { }
    }
}
