using System;
using System.Runtime.CompilerServices;

namespace Ponito.Core.Asyncronized.Interfaces
{
    public interface Completable : INotifyCompletion
    {
        bool IsCompleted { get; }
    }
}