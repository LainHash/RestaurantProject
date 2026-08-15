using System.Net;

namespace Restaurant.Domain.Models.Results
{
    public class Result
    {
        public bool IsSucceed { get; protected set; }
        public string Message { get; protected set; }
        public int StatusCode { get; protected set; }

        public Result(bool isSucceed, string message, HttpStatusCode statusCode)
        {
            IsSucceed = isSucceed;
            Message = message;
            StatusCode = (int)statusCode;
        }

        public static Result Succeed(string message, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(true, message, statusCode);

        public static Result Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(false, message, statusCode);
    }
    public class Result<T> : Result
    {
        public T? Data { get; }

        protected Result(T? data, bool isSucceed, string message, HttpStatusCode statusCode)
            : base(isSucceed, message, statusCode)
        {
            Data = data;
        }

        public static Result<T> Succeed(T? data, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(data, true, message, statusCode);

        public new static Result<T> Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(default, false, message, statusCode);

    }
}
