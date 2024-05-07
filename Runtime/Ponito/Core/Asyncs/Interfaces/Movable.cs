using System;

namespace Ponito.Core.Asyncs.Compilations
{
    public interface Movable
    {
        bool   MoveNext();
        void   OnCompleted(Action continuation);
        Action Continuation { get; }
        bool   IsCompleted  { get; }
    }
}