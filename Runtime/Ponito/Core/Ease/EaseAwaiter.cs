using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;

namespace Ponito.Core.Ease
{
    public readonly struct EaseAwaiter : Movable
    {
        private readonly Easable ease;

        public EaseAwaiter(Easable ease) => this.ease = ease;
        
        public bool IsCompleted => ease.IsCompleted;

        public bool MoveNext() => ease.MoveNext();

        public void OnCompleted(Action continuation) => ease.OnCompleted(continuation);

        public void GetResult()
        {
        }
    }
}