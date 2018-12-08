using System;
using System.Collections.Generic;

namespace Fp
{
    public static partial class Option
    {
        public static Option<TOut> Then<TIn, TOut>(
            this Option<TIn> option,
            Func<TIn, Option<TOut>> binder)
        {
            return option.IsSome
                ? binder(option.Value)
                : Option.None<TOut>();
        }

        public static Option<TOut> Then<TIn, TOut>(
            this Option<TIn> option,
            Func<TIn, TOut> mapping)
        {
            return option.Then(v => Option.Of(() => mapping(v)));
        }

        public static Option<Unit> Then<TIn>(
            this Option<TIn> option,
            Action<TIn> mapping)
        {
            return option.Then(v => Option.Of(() => mapping(v)));
        }

        public static Option<T> OnNone<T>(
            this Option<T> option,
            Action action)
        {
            if (!option.IsSome) action();
            return option;
        }
            
        public static List<T> ToList<T>(this Option<T> option)
        {
            var result = new List<T>();
            if (option.IsSome) result.Add(option.Value);
            return result;
        }
    }
}