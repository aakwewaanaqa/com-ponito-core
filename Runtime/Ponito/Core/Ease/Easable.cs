using System;
using System.Collections;
using Ponito.Core.Asyncs;
using Ponito.Core.Asyncs.Compilations;

namespace Ponito.Core.Ease
{
    public interface Easable : Awaitable<EaseAwaiter>, IDisposable
    {
        public float Progress { get; }
        public void  Kill();
    }
}