using System;
using System.Runtime.CompilerServices;

namespace Ponito.Core.Ease
{
    public readonly struct EaseAwaiter : INotifyCompletion
    {
        private readonly Easable ease;

        public EaseAwaiter(Easable ease) => this.ease = ease;

        public void OnCompleted(Action continuation) => ease.OnCompleted(continuation);

        public void GetResult()
        {
        }

        public bool IsCompleted => ease?.IsCompleted ?? false;
    }
}