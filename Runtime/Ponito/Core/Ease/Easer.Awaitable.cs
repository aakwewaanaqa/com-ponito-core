using System;

namespace Ponito.Core.Ease
{
    internal partial class Easer<T>
    {
        public EaseAwaiter GetAwaiter()                     => new(this);
        public bool        IsCompleted                      => isEnded || time >= duration;
        public void        OnCompleted(Action continuation) => this.continuation = continuation;
    }
}