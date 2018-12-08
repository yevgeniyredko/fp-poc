using System;

namespace Fp
{
    public static partial class Result
    {
        public static Result<TOut> Then<TInput, TOut>(
            this Result<TInput> input,
            Func<TInput, Result<TOut>> binder)
        {
            return input.IsSuccess
                ? binder(input.Value)
                : Result.Fail<TOut>(input.Error);
        }
        
        public static Result<TOut> Then<TIn, TOut>(
            this Result<TIn> input,
            Func<TIn, TOut> mapping)
        {
            return input.Then(v => Result.Of(() => mapping(v)));
        }

        public static Result<Unit> Then<TIn>(
            this Result<TIn> input,
            Action<TIn> mapping)
        {
            return input.Then(v => Result.Of(() => mapping(v)));
        }

        public static Result<T> OnFail<T>(
            this Result<T> input,
            Action<string> handleError)
        {
            if (!input.IsSuccess) handleError(input.Error);
            return input;
        }

        public static Result<T> MapError<T>(
            this Result<T> input,
            Func<string, string> mapping)
        {
            return input.IsSuccess
                ? input
                : Result.Fail<T>(mapping(input.Error));
        }
    }
}