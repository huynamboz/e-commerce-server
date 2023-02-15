using e_commerce_server.src.Packages.HttpException;
using System.Net;

namespace e_commerce_server.src.Packages.HttpException
{
    public class InternalException : HttpException
    {
        public InternalException(string message = "Internal server error") : base(HttpStatusCode.InternalServerError, ErrorEnum.INTERNAL, message) { }
    }
}
