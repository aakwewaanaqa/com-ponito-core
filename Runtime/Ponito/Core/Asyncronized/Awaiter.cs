using System.Runtime.CompilerServices;

namespace Ponito.Core.Asyncronized
{
    public interface Awaiter : INotifyCompletion
    {
        bool    IsCompleted { get; }
        void    GetResult();
        Awaiter GetAwaiter() => this;
    }
}