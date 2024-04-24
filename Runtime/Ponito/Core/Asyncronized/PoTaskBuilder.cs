using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Ponito.Core.Asyncronized
{
    public struct PoTaskBuilder
    {
        private Exception ex   { get; set; }
        private PoTask    task { get; set; }

        public static PoTaskBuilder Create()
        {
            return default;
        }

        public PoTask Task => task;

        public void SetException(Exception ex) => this.ex = ex;

        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        public void SetResult()
        {
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            Debug.Log(nameof(SetStateMachine));
        }
    }

    public struct PoTaskBuilder<TResult>
    {
        private Exception       ex   { get; set; }
        private PoTask<TResult> task { get; set; }

        public static PoTaskBuilder<TResult> Create()
        {
            return default;
        }

        public PoTask<TResult> Task => task;

        public void SetException(Exception ex) => this.ex = ex;

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
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            Debug.Log(nameof(SetStateMachine));
        }
    }
}