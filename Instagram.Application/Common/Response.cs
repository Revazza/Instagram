using FluentValidation.Results;
using Instagram.Domain.Chats.Entities;

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
    public List<string> Errors { get; set; }


    private Response(string message, ResponseStatus status)
    {
        Message = message;
        Status = status;
        Payload = new Dictionary<string, object>();
        Errors = new List<string>();
    }

    private Response(ResponseStatus status)
    {
        Status = status;
        Payload = new Dictionary<string, object>();
        Errors = new List<string>();
    }

    public Response()
    {
        Payload = new Dictionary<string, object>();
        Errors = new List<string>();
    }

    public Response Add(string key, object value)
    {
        Payload.Add(key, value);
        return this;
    }
    public Response AddError(string error)
    {
        Errors.Add(error);
        return this;
    }

    public object? Get(string key)
    {
        if(!Payload.TryGetValue(key, out object? obj))
        {
            return null;
        }
        return obj;
    }

    public T? Get<T>(string key) where T : class
    {
        if (!Payload.TryGetValue(key, out object? obj))
        {
            return null;
        }

        if(obj is not T)
        {
            return null;
        }

        return obj as T;
    }    

    public static Response Error(string errorMsg)
    {
        return new Response(ResponseStatus.Error).AddError(errorMsg);
    }

    public static Response Ok(string message = "")
    {
        return new Response(message, ResponseStatus.Ok);
    }

    public static Response AddFluentValidationErrors(List<ValidationFailure> errors)
    {
        return new Response(ResponseStatus.Error)
        {
            Errors = errors.Select(error => error.ErrorMessage).ToList()
        };
    }


}