using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncronized;

namespace Ponito.Core.Ease
{
    public readonly struct EaseAwaiter : Awaiter
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