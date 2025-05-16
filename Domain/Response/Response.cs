using System.Net;

namespace Domain.Response;

public class Response<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public Response(T? data)
    {
        IsSuccess = true;
        StatusCode = 200;
        Data = data;
        Message = string.Empty;
    }

    public Response(HttpStatusCode statusCode, string message)
    {
        IsSuccess = true;
        StatusCode = (int)statusCode;
        Data = default;
        Message = message;
    }
}