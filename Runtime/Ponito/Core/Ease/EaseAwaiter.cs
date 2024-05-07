using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Compilations;

namespace Ponito.Core.Ease
{
    public readonly struct EaseAwaiter : INotifyCompletion, Movable
    {
        private readonly Easable ease;

        public EaseAwaiter(Easable ease) => this.ease = ease;
        
        public bool IsCompleted => ease.IsCompleted;

        public bool MoveNext() => ease.MoveNext();

        public void OnCompleted(Action continuation) => ease.OnCompleted(continuation);

        public Action Continuation => ease.Continuation;

        public void GetResult()
        {
        }
    }
}