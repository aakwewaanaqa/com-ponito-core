using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncronized.Interfaces;
using Ponito.Core.Asyncronized.Runner;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncronized
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder<>))]
    public struct PoTask<TResult> : Awaitable<TResult>
    {
        private Completable awaiter { get; set; }
        private TResult     result  { get; set; }

        public bool IsCompleted
        {
            get
            {
                typeof(PoTask<>).Get(nameof(IsCompleted));
                var flag = awaiter is null || awaiter.IsCompleted;
                return flag;
            }
        }

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

        public static void Create<TAwaiter>(
            IAsyncStateMachine machine,
            ref PoTask<TResult> task,
            ref TAwaiter completable,
            Action continuation)
            where TAwaiter : Completable
        {
            machine.GetType().F(nameof(Create));

            task.awaiter = completable;
            PoTaskRunner.Instance.Add(completable, continuation);
        }
    }
}