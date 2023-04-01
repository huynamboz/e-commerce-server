﻿using e_commerce_server.Src.Packages.HttpException;
using System.Net;

namespace e_commerce_server.Src.Packages.HttpException
{
    public class BadRequestException : HttpException
    {
        public BadRequestException(string message = "You don't have permission to access this resource"):base(HttpStatusCode.BadRequest, ErrorEnum.BAD_REQUEST, message) { }
    }
}