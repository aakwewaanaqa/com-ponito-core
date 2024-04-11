using System;
using Cysharp.Threading.Tasks;

namespace Ponito.Core.Ease
{
    public interface EaseAnimation : IDisposable
    {
        float           Progress { get; }
        void            Kill();
        bool            IsPlaying();
        UniTask.Awaiter GetAwaiter();
    }
}