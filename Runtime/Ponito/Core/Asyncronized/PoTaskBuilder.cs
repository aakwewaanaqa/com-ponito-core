using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncronized.Interfaces;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncronized
{
    public struct PoTaskBuilder
    {
        private Exception ex { get; set; }
        private PoTask    task;

        public static PoTaskBuilder Create()
        {
            return default;
        }

        public PoTask Task => task;

        public void SetException(Exception ex)
        {
            this.ex = ex;
        }

        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            // stateMachine.GetType().F(nameof(Start));
            stateMachine.MoveNext();
        }

        public void SetResult()
        {
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : Completable
            where TStateMachine : IAsyncStateMachine
        {
            "await".Keyword(awaiter.GetType().Name);
            // typeof(PoTaskBuilder).F(nameof(AwaitOnCompleted));
            PoTask.Create(stateMachine, ref task, ref awaiter, stateMachine.MoveNext);
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