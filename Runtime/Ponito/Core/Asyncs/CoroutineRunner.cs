using System.Collections;
using Ponito.Core.Asyncs.Promises;

namespace Ponito.Core.Asyncs
{
    public class CoroutineRunner : MonoSingleton<CoroutineRunner>
    {
        /// <inheritdoc />
        protected override bool IsInitialized { get; }

        /// <inheritdoc />
        protected override bool IsDontDestroyOnLoad { get; }

        /// <inheritdoc />
        protected override void Initialize()
        {
        }

        public void Run(IEnumerator ie, out Promise p)
        {
            p = new Promise();
            StartCoroutine(YieldPromise(ie, p));
        }

        private IEnumerator YieldPromise(IEnumerator ie, Promise p)
        {
            yield return p;
            yield return StartCoroutine(ie);
            p.State = PromiseState.Done;
        }
    }
}