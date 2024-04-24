using System.Runtime.CompilerServices;

namespace Ponito.Core.Asyncronized
{
    public interface Awaiter : INotifyCompletion
    {
        public bool IsCompleted { get; }
    }
}