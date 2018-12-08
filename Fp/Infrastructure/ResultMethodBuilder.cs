using System;
using System.Runtime.CompilerServices;

namespace Fp.Infrastructure
{
    public class ResultMethodBuilder<T>
    {
        private Result<T> result = Result.Fail<T>("");
        public Result<T> Task => result;
        
        public static ResultMethodBuilder<T> Create()
        {
            return new ResultMethodBuilder<T>();
        }
        
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        public void SetResult(T result)
        {
            this.result = Result.Ok(result);
        }

        public void SetException(Exception exception)
        {
            this.result = Result.Fail<T>(exception.Message);
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {   
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
        }
    }
}