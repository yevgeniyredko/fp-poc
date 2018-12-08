using System;
using System.Runtime.CompilerServices;

namespace Fp.Infrastructure
{
    public class OptionAwaiter<T> : INotifyCompletion
    {
        private readonly Option<T> option;

        public OptionAwaiter(Option<T> option)
        {
            this.option = option;
        }

        public bool IsCompleted => option.IsSome;
        public T GetResult() => option.Value;
        public void OnCompleted(Action continuation) { }
    }
}