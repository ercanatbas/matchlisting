using System.Linq;
using System.Net;
using FluentValidation;
using MatchList.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MatchList.Api.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            if (context.Exception.GetType() == typeof(ValidationCustomException))
            {
                if (context.Exception.InnerException?.GetType() == typeof(ValidationException))
                {
                    var validationExceptions = ((ValidationException) context.Exception.InnerException).Errors.Select(x => x.ErrorMessage).ToArray();
                    var errorList            = validationExceptions.Select(errorMessage => new Error { Message = errorMessage }).ToArray();
                    var errorResponse        = new JsonErrorResponse { Errors = errorList };
                    errorResponse.Errors = errorResponse.Errors.Where(e => !string.IsNullOrEmpty(e.Message)).ToArray();

                    context.Result                          = new BadRequestObjectResult(errorResponse);
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                }
            }
            else
            {
                var json = new JsonErrorResponse
                           {
                               Errors = new[]
                                        {
                                            new Error
                                            {
                                                Message = context.Exception.Message,
                                                Code    = context.Exception.Data["Code"]?.ToString()
                                            },
                                            new Error
                                            {
                                                Message = context.Exception.InnerException?.Message,
                                                Code    = context.Exception.InnerException?.Data["Code"]?.ToString()
                                            }
                                        }
                           };
                json.Errors = json.Errors.Where(e => !string.IsNullOrEmpty(e.Message)).ToArray();

                if (context.Exception.GetType() == typeof(ApplicationCustomException))
                {
                    context.Result                          = new BadRequestObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                }
                else
                {
                    context.Result                          = new InternalServerErrorObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                }
            }

            context.ExceptionHandled = true;
        }
    }

    public record JsonErrorResponse
    {
        public Error[] Errors { get; set; }
    }

    public record Error
    {
        public string Code    { get; set; }
        public string Message { get; set; }
    }

    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error) : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}