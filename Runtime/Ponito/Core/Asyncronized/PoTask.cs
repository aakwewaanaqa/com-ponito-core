using System;
using System.Runtime.CompilerServices;

namespace Ponito.Core.Asyncronized
{
    [AsyncMethodBuilderAttribute(typeof(PoTaskBuilder))]
    public struct PoTask : INotifyCompletion
    {
        public bool IsCompleted { get; set; }

        public PoTask GetAwaiter()
        {
            return this;
        }

        public void GetResult()
        {
        }

        public void OnCompleted(Action continuation)
        {
            continuation?.Invoke();
        }
    }

    [AsyncMethodBuilderAttribute(typeof(PoTaskBuilder<>))]
    public struct PoTask<TResult> : INotifyCompletion
    {
        private TResult result      { get; set; }
        public  bool    IsCompleted { get; }

        public PoTask<TResult> GetAwaiter()
        {
            return this;
        }

        public void    SetResult(TResult result) => this.result = result;
        public TResult GetResult()               => result;

        public void OnCompleted(Action continuation)
        {
            continuation?.Invoke();
        }
    }
}