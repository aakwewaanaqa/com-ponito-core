using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Ponito.Core.Asyncronized
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public readonly struct PoTask : ICriticalNotifyCompletion
    {
        private Awaiter awaiter { get; }

        public bool IsCompleted => awaiter.IsCompleted;
        
        public PoTask(IEnumerator ie)
        {
            awaiter = new Awaiter(ie);
        }
        
        public Awaiter GetAwaiter()
        {
            return awaiter;
        }

        public void GetResult()
        {
            awaiter.GetResult();
        }
        
        public void OnCompleted(Action continuation)
        {
            awaiter.OnCompleted(continuation);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            awaiter.UnsafeOnCompleted(continuation);
        }

        public struct Awaiter : ICriticalNotifyCompletion
        {
            public bool IsCompleted { get; private set; }

            public Awaiter(IEnumerator ie)
            {
                IsCompleted = false;
                PoTaskRunner.Instance.StartCoroutine(RunIEnumerator(ie));
            }

            private IEnumerator RunIEnumerator(IEnumerator ie)
            {
                yield return PoTaskRunner.Instance.StartCoroutine(ie);
                IsCompleted = true;
            }

            public void OnCompleted(Action continuation)
            {
                if (IsCompleted) continuation?.Invoke();
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                if (IsCompleted) continuation?.Invoke();
            }

            public void GetResult()
            {
            }
        }
    }
}