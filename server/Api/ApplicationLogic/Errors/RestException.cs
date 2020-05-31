using System;
using System.Collections.Generic;
using System.Net;

namespace Api.ApplicationLogic.Errors
{
    public class RestException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }
        public string ErrorDescription { get; private set; }
        public Dictionary<string, string> Errors { get; set; }

        public RestException(HttpStatusCode httpStatusCode, string errorDescription) : base(errorDescription)
        {
            HttpStatusCode = httpStatusCode;
            ErrorDescription = errorDescription;
        }
    }
}