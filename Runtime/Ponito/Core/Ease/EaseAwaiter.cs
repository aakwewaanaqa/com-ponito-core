using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncronized;
using Ponito.Core.Asyncronized.Interfaces;

namespace Ponito.Core.Ease
{
    public readonly struct EaseAwaiter : Awaitable
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