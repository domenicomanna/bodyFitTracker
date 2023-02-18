using System;
using System.Collections.Generic;
using System.Net;

namespace Api.Common.Exceptions;

public class RestException : Exception
{
    public HttpStatusCode HttpStatusCode { get; private set; }
    public Dictionary<string, string> Errors { get; private set; }

    public RestException() { }

    public RestException(HttpStatusCode httpStatusCode, string error)
    {
        HttpStatusCode = httpStatusCode;
        Errors = new Dictionary<string, string>();
        Errors.Add("", error);
    }

    public RestException(HttpStatusCode httpStatusCode, Dictionary<string, string> errors)
    {
        HttpStatusCode = httpStatusCode;
        Errors = errors;
    }
}
