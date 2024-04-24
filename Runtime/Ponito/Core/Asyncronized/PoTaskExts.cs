using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncronized.Interfaces;
using UnityEngine;

namespace Ponito.Core.Asyncronized
{
    public static class PoTaskExts
    {
        public static AsyncOperationAwaiter GetAwaiter(this AsyncOperation op)
        {
            return new AsyncOperationAwaiter(op);
        }

        public readonly struct AsyncOperationAwaiter : Awaitable
        {
            private AsyncOperation op { get; }
            
            public AsyncOperationAwaiter(AsyncOperation op)
            {
                this.op = op;
            }

            public void GetResult()
            {
                
            }
            
            public bool IsCompleted => op.isDone;
            
            public void OnCompleted(Action continuation)
            {
                continuation();
            }
        }
    }
}