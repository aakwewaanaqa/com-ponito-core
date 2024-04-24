using System;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;

namespace Ponito.Core.Asyncronized.Awaiters
{
    public class WhenAwaiter : Awaiter, IDisposable
    {
        private WhenPredicate predicate    { get; set; }
        private Action        continuation { get; set; }

        public WhenAwaiter([NotNull] WhenPredicate predicate)
        {
            this.predicate = predicate;
        }
        
        public void OnCompleted([NotNull] Action continuation)
        {
            this.continuation = continuation;
            PoTaskRunner.Instance.Enqueue(this);
        }

        public bool IsCompleted
        {
            get
            {
                var isCompleted = predicate();
                if (isCompleted) continuation();
                return isCompleted;
            }
        }

        public void GetResult()
        {
            
        }

        public void Dispose()
        {
            predicate    = null;
            continuation = null;
        }

        public Awaiter GetAwaiter() => this;
    }
}