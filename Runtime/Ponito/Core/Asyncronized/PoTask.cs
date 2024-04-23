using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
}

namespace Ponito.Core.Asyncronized
{
    [AsyncMethodBuilderAttribute(typeof(PoTaskBuilder))]
    public struct PoTask : INotifyCompletion
    {
        public bool IsCompleted { get; private set; }

        private IEnumerator Run(IEnumerator ie)
        {
            yield return PoTaskRunner.Instance.StartCoroutine(ie);
            IsCompleted = true;
        }

        public PoTask(IEnumerator ie)
        {
            IsCompleted = false;
            PoTaskRunner.Instance.StartCoroutine(Run(ie));
        }

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