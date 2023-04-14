using System.Net;

namespace e_commerce_server.src.Packages.HttpExceptions
{
    public class BadRequestException : HttpException
    {
        public BadRequestException(string message = "Bad request"):base(HttpStatusCode.BadRequest, ErrorEnum.BAD_REQUEST, message) { }
    }
}
