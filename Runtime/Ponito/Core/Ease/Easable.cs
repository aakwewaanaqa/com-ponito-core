using System;
using System.Collections;

namespace Ponito.Core.Ease
{
    public interface Easable : IDisposable, IEnumerator
    {
        public bool        IsCompleted { get; }
        public float       Progress    { get; }
        public void        Kill();
        public EaseAwaiter GetAwaiter();
    }
}