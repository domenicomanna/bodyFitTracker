using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.ApplicationLogic.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (exception is RestException)
            {
                _logger.LogError(exception, "Rest exception");
                RestException restException = exception as RestException;
                errors = restException.Errors;
                context.Response.StatusCode = (int)restException.HttpStatusCode;
            }
            else
            {
                _logger.LogError(exception, "server error");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            string errorsAsJson = JsonConvert.SerializeObject(errors);
            await context.Response.WriteAsync(errorsAsJson);
        }
    }
}
