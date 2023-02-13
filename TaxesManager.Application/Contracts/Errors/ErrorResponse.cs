using System.Text.Json.Serialization;

namespace TaxesManager.Application.Contracts.Errors
{
    public class ErrorResponse
    {
        public string Reason { get; set; }
        public string Message { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }

        public ErrorResponse(string message, string reason, int statusCode)
        {
            Message = message;
            Reason = reason;
            StatusCode = statusCode;
        }
    }
}
