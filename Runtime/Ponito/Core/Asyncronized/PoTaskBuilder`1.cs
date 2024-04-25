using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncronized.Interfaces;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncronized
{
    public struct PoTaskBuilder<TResult>
    {
        private Exception       ex { get; set; }
        private PoTask<TResult> task;

        public static PoTaskBuilder<TResult> Create()
        {
            return default;
        }

        public PoTask<TResult> Task => task;

        public void SetException(Exception ex)
        {
            this.ex = ex;
        }

        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        public void SetResult(TResult result)
        {
            task.SetResult(result);
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : Completable
            where TStateMachine : IAsyncStateMachine
        {
            "await".Keyword(awaiter.GetType().Name);
            typeof(PoTaskBuilder).F(nameof(AwaitOnCompleted));
            PoTask<TResult>.Create(stateMachine, ref task, ref awaiter, stateMachine.MoveNext);
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : Completable
            where TStateMachine : IAsyncStateMachine
        {
            AwaitOnCompleted(ref awaiter, ref stateMachine);
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }
}