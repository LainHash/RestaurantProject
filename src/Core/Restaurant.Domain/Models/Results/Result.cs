using System.Net;

namespace Restaurant.Domain.Models.Results
{
    public class Result<T>
    {
        public bool IsSucceed { get; }
        public string Message { get; }
        public int StatusCode { get; }
        public T? Data { get; }

        protected Result(T? data, bool isSucceed, string message, HttpStatusCode statusCode)
        {
            Data = data;
            IsSucceed = isSucceed;
            Message = message;
            StatusCode = (int)statusCode;
        }

        public static Result<T> Succeed(T? data, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(data, true, message, statusCode);

        public static Result<T> Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(default, false, message, statusCode);

    }
}
