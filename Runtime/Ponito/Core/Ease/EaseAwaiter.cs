using System;
using System.Runtime.CompilerServices;

namespace Ponito.Core.Ease
{
    public readonly struct EaseAwaiter : INotifyCompletion
    {
        private readonly EaseAnimation ease;

        public EaseAwaiter(EaseAnimation ease)
        {
            this.ease = ease;
            this.ease.Play();
        }

        public void GetResult()
        {
        }

        public bool IsCompleted => !ease.IsPlaying;

        public void OnCompleted(Action continuation)
        {
            continuation();
        }
    }
}