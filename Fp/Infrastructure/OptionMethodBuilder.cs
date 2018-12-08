using System;
using System.Runtime.CompilerServices;

namespace Fp.Infrastructure
{
    public class OptionMethodBuilder<T>
    {
        private Option<T> option = Option.None<T>();
        public Option<T> Task => option;
        
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
            this.option = Option.Some(result);
        }

        public void SetException(Exception exception)
        {
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