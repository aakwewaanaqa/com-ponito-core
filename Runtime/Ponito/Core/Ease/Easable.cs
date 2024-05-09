using System;
using System.Collections;
using Ponito.Core.Asyncs;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;

namespace Ponito.Core.Ease
{
    public interface Easable : IEnumerator, IDisposable
    {
        public float   Progress { get; }
        public void    Kill();
        public Movable GetAwaiter();
    }
}