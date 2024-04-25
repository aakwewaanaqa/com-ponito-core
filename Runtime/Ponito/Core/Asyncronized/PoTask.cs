using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Ponito.Core.Asyncronized.Interfaces;
using Ponito.Core.Asyncronized.Runner;
using Ponito.Core.DebugHelper;
using UnityEngine;

namespace Ponito.Core.Asyncronized
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public struct PoTask : Awaitable
    {
        private Completable awaiter { get; set; }

        public bool IsCompleted
        {
            get
            {
                if (awaiter is null) return true;
                else return awaiter.IsCompleted;
            }
        }

        public void OnCompleted([NotNull] Action continuation)
        {
            continuation();
        }

        public void GetResult()
        {
        }

        public PoTask GetAwaiter()
        {
            return this;
        }

        public static void Await<TAwaiter>(IAsyncStateMachine machine,
                                           ref PoTask task,
                                           ref TAwaiter completable,
                                           Action continuation)
            where TAwaiter : Completable
        {
            typeof(PoTask).F(nameof(Await));
            task.awaiter = completable;
            PoTaskRunner.Instance.Enqueue(completable, continuation);
        }
    }

    [AsyncMethodBuilder(typeof(PoTaskBuilder<>))]
    public struct PoTask<TResult> : Awaitable<TResult>
    {
        private Completable awaiter     { get; set; }
        private TResult     result      { get; set; }
        public  bool        IsCompleted => awaiter is null || awaiter.IsCompleted;

        public TResult GetResult()
        {
            return result;
        }

        public void OnCompleted(Action continuation)
        {
            continuation();
        }

        public PoTask<TResult> GetAwaiter()
        {
            return this;
        }

        public void SetResult(TResult result)
        {
            this.result = result;
        }

        public void Await(Completable completable, Action continuation)
        {
            awaiter = completable;
            PoTaskRunner.Instance.Enqueue(awaiter, continuation);
        }
    }
}