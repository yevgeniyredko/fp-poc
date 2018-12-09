using System;
using System.Runtime.CompilerServices;
using Fp.Infrastructure;

namespace Fp
{
    [AsyncMethodBuilder(typeof(AsyncOptionMethodBuilder<>))]
    public struct Option<T>
    {
        public Option(bool isSome, T value = default)
        {
            IsSome = isSome;
            Value = value;
        }
        
        public bool IsSome { get; }
        public T Value { get; }
        public OptionAwaiter<T> GetAwaiter() => new OptionAwaiter<T>(this);
    }

    public static partial class Option
    {
        public static Option<Unit> Some() => Some<Unit>(null);
        public static Option<Unit> None() => None<Unit>();
        public static Option<T> Some<T>(T value) => new Option<T>(true, value);
        public static Option<T> None<T>() => new Option<T>(false);
        public static Option<T> AsOption<T>(this T value) => Some(value);

        public static Option<T> Of<T>(Func<T> f)
        {
            try
            {
                return Some(f());
            }
            catch (Exception)
            {
                return None<T>();
            }
        }

        public static Option<Unit> Of(Action f)
        {
            try
            {
                f();
                return Some();
            }
            catch (Exception)
            {
                return None();
            }
        }
    }
}