﻿using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncronized.Interfaces;
using Ponito.Core.Asyncronized.Runner;
using UnityEngine;

namespace Ponito.Core.Asyncronized
{
    public struct PoTaskBuilder
    {
        private Exception ex   { get; set; }
        private PoTask    task { get; }

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
            stateMachine.MoveNext();
        }

        public void SetResult()
        {
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : Completable
            where TStateMachine : IAsyncStateMachine
        {
            // awaiter.OnCompleted(stateMachine.MoveNext);
            PoTaskRunner.Instance.Enqueue(awaiter, stateMachine.MoveNext);
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : Completable
            where TStateMachine : IAsyncStateMachine
        {
            // awaiter.OnCompleted(stateMachine.MoveNext);
            PoTaskRunner.Instance.Enqueue(awaiter, stateMachine.MoveNext);
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }

    public struct PoTaskBuilder<TResult>
    {
        private Exception       ex   { get; set; }
        private PoTask<TResult> task { get; }

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
            // awaiter.OnCompleted(stateMachine.MoveNext);
            PoTaskRunner.Instance.Enqueue(awaiter, stateMachine.MoveNext);
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : Completable
            where TStateMachine : IAsyncStateMachine
        {
            // awaiter.OnCompleted(stateMachine.MoveNext);
            PoTaskRunner.Instance.Enqueue(awaiter, stateMachine.MoveNext);
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            Debug.Log(nameof(SetStateMachine));
        }
    }
}