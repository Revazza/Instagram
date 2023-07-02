using FluentValidation.Results;
using System.Runtime.CompilerServices;

namespace Instagram.Application.Common;

public enum ResponseStatus
{
    Ok,
    Error
}

public class Response
{
    public ResponseStatus Status { get; set; } = ResponseStatus.Ok;
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, object> Payload { get; set; }

    public Response()
    {
        Payload = new Dictionary<string, object>();
    }

    public Response(string message)
    {
        Message = message;
        Payload = new Dictionary<string, object>();
    }

    public Response Add(string key, object value)
    {
        Payload.Add(key, value);
        return this;
    }

    public Response IsFailure(string errorMsg)
    {
        Status = ResponseStatus.Error;
        Message = errorMsg;
        return this;
    }

    public Response AddFluentValidationErrors(List<ValidationFailure> errors)
    {
        Status = ResponseStatus.Error;
        Payload.Add("errors", errors.Select(error => error.ErrorMessage).ToList());
        return this;
    }


}