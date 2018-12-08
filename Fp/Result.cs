using System;
using System.Runtime.CompilerServices;
using Fp.Infrastructure;

namespace Fp
{
    [AsyncMethodBuilder(typeof(ResultMethodBuilder<>))]
    public struct Result<T>
    {
        public Result(string error, T value = default(T))
        {
            Error = error;
            Value = value;
        }

        public string Error { get; }
        public T Value { get; }
        public bool IsSuccess => Error == null;
        public ResultAwaiter<T> GetAwaiter() => new ResultAwaiter<T>(this);
    }

    public static partial class Result
    {
        public static Result<Unit> Ok() => Ok<Unit>(null);
        public static Result<Unit> Fail(string e) => Fail<Unit>(e);
        public static Result<T> Ok<T>(T value) => new Result<T>(null, value);
        public static Result<T> Fail<T>(string e) => new Result<T>(e);
        public static Result<T> AsResult<T>(this T value) => Ok(value);

        public static Result<T> Of<T>(Func<T> f, string error = null)
        {
            try
            {
                return Ok(f());
            }
            catch (Exception e)
            {
                return Fail<T>(error ?? e.Message);
            }
        }

        public static Result<Unit> Of(Action f, string error = null)
        {
            try
            {
                f();
                return Ok();
            }
            catch (Exception e)
            {
                return Fail(error ?? e.Message);
            }
        }
    }
}