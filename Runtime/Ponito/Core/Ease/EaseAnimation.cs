using System;
using Cysharp.Threading.Tasks;
using Ponito.Core.Asyncronized;

namespace Ponito.Core.Ease
{
    public interface EaseAnimation : IDisposable
    {
        float       Progress { get; }
        void        Kill();
        bool        IsPlaying { get; }
        void        Play();
        EaseAwaiter GetAwaiter() => new(this);
    }
}