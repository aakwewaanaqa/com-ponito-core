using System;
using JetBrains.Annotations;
using Ponito.Core.Asyncronized.Enums;
using Ponito.Core.Asyncronized.Interfaces;

namespace Ponito.Core.Asyncronized.Awaiters
{
    public struct DoUntil : Awaitable, Runnable, IDisposable
    {
        private BoolPredicate predicate { get; set; }
        public  RunMark       Mark      { get; private set; }

        public DoUntil([NotNull] BoolPredicate predicate)
        {
            this.predicate = predicate;
            Mark           = RunMark.New;
        }

        public void OnCompleted([NotNull] Action continuation)
        {
            continuation();
        }

        public bool IsCompleted => Mark is RunMark.Done;

        public bool Run()
        {
            if (IsCompleted) return true;
            Mark = predicate() ? RunMark.Done : RunMark.AtRunner;
            return IsCompleted;
        }

        public void GetResult()
        {
        }

        public void Dispose()
        {
            predicate = null;
        }

        public Awaitable GetAwaiter()
        {
            return this;
        }
    }
}