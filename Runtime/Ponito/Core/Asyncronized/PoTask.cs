using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncronized.Interfaces;

namespace Ponito.Core.Asyncronized
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public struct PoTask : Awaitable
    {
        public bool IsCompleted => true;

        public void OnCompleted(Action continuation)
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
    }

    [AsyncMethodBuilder(typeof(PoTaskBuilder<>))]
    public struct PoTask<TResult> : Awaitable<TResult>
    {
        private TResult result      { get; set; }
        public  bool    IsCompleted => true;

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
    }
}