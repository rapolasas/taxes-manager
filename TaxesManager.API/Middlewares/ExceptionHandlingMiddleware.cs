using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TaxesManager.Application.Contracts.Errors;
using TaxesManager.Domain.Exceptions;

namespace TaxesManager.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private const string ReasonMessageTemplate = "{0} was thrown";

        private readonly JsonSerializerSettings _settings;

        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                httpContext.Response.Clear();
                var error = GetError(ex);
                httpContext.Response.StatusCode = error.StatusCode;
                httpContext.Response.ContentType = "application/json";
                await HttpResponseWritingExtensions.WriteAsync(httpContext.Response, JsonConvert.SerializeObject(error, Formatting.Indented, _settings));
            }
        }

        private ErrorResponse GetError(Exception exception)
        {
            if(exception is ValidationException)
            {
                return new ErrorResponse(exception.Message, string.Format(ReasonMessageTemplate, nameof(ValidationException)), StatusCodes.Status400BadRequest);
            }
            else if(exception is NotFoundException)
            {
                return new ErrorResponse(exception.Message, string.Format(ReasonMessageTemplate, nameof(NotFoundException)), StatusCodes.Status404NotFound);
            }
            else if (exception is DuplicateException)
            {
                return new ErrorResponse(exception.Message, string.Format(ReasonMessageTemplate, nameof(DuplicateException)), StatusCodes.Status400BadRequest);
            }
            else if (exception is UnsupportedTaxScheduleException)
            {
                return new ErrorResponse(exception.Message, string.Format(ReasonMessageTemplate, nameof(UnsupportedTaxScheduleException)), StatusCodes.Status400BadRequest);
            }
            return new ErrorResponse("Internal server error", "Unexpected internal server error", StatusCodes.Status500InternalServerError);
        }
    }
}
