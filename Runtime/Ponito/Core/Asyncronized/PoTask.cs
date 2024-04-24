using System;
using System.Runtime.CompilerServices;

namespace Ponito.Core.Asyncronized
{
    [AsyncMethodBuilderAttribute(typeof(PoTaskBuilder))]
    public readonly struct PoTask : INotifyCompletion
    {
        public bool IsCompleted { get; }

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
}