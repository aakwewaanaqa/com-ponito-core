using System;
using Ponito.Core.Asyncs.Interfaces;

namespace Ponito.Core.Ease
{
    internal partial class Easer<T> : Easable
    {
        public Movable GetAwaiter()                     => this;
        public bool    IsCompleted                      => isEnded || time >= duration;
        public void    OnCompleted(Action continuation) => this.continuation = continuation;

        public void GetResult()
        {
        }
    }
}