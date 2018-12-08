using System;
using System.Runtime.CompilerServices;

namespace Fp.Infrastructure
{
    public class ResultAwaiter<T> : INotifyCompletion
    {
        private readonly Result<T> result;

        public ResultAwaiter(Result<T> result)
        {
            this.result = result;
        }

        public bool IsCompleted => result.IsSuccess;
        public T GetResult() => result.Value;
        public void OnCompleted(Action continuation) { }
    }
}