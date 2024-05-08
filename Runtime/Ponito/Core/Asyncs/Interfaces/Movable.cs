using System.Runtime.CompilerServices;

namespace Ponito.Core.Asyncs.Interfaces
{
    public interface Movable : INotifyCompletion
    {
        bool   MoveNext();
        bool   IsCompleted  { get; }
        void   GetResult();
    }
}