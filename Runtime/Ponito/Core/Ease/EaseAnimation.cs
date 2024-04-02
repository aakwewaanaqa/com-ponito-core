using System;
using Cysharp.Threading.Tasks;

namespace Ponito.Core.Ease
{
    public interface EaseAnimation : IDisposable
    {
        void  Kill();
        bool  IsPlaying();
        float Progress { get; }
        UniTask.Awaiter GetAwaiter();
    }
}