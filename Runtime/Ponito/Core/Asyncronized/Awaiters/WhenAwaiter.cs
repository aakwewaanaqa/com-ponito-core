using System;
using System.Threading;
using JetBrains.Annotations;
using Ponito.Core.Asyncronized.Interfaces;
using Ponito.Core.Asyncronized.Runner;
using UnityEngine;

namespace Ponito.Core.Asyncronized.Awaiters
{
    public class WhenAwaiter : Awaitable, IDisposable
    {
        private WhenPredicate predicate { get; set; }

        public WhenAwaiter([NotNull] WhenPredicate predicate)
        {
            this.predicate = predicate;
        }

        public void OnCompleted([NotNull] Action continuation)
        {
            continuation();
        }

        public bool IsCompleted => predicate();

        public void GetResult()
        {
        }

        public void Dispose()
        {
            predicate = null;
        }

        public Awaitable GetAwaiter() => this;
    }
}