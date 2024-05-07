using System;

namespace Ponito.Core.Asyncs.Compilations
{
    public interface Movable
    {
        bool MoveNext();
        void OnCompleted(Action continuation);
    }
}