using System.Net;

namespace e_commerce_server.src.Packages.HttpExceptions
{
    public class DuplicateException : HttpException
    {
        public DuplicateException(string message  = "Duplicate record"): base(HttpStatusCode.Conflict, ErrorEnum.DUPLICATE, message) { }
    }
}
