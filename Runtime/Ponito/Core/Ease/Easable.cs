using System;
using System.Collections;
using Ponito.Core.Asyncs;

namespace Ponito.Core.Ease
{
    public interface Easable : Awaitable<EaseAwaiter>, IDisposable, IEnumerator
    {
        public float Progress { get; }
        public void  Kill();
    }
}