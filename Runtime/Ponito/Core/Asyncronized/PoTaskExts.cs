using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Ponito.Core.Asyncronized
{
    public static class PoTaskExts
    {
        public static AsyncOperationAwaiter GetAwaiter(this AsyncOperation op)
        {
            return new AsyncOperationAwaiter(op);
        }

        public readonly struct AsyncOperationAwaiter : Awaiter
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