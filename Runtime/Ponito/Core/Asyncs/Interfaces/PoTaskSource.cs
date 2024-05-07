using System;
using Ponito.Core.Asyncs.Tasks;

namespace Ponito.Core.Asyncs.Compilations
{
    public interface PoTaskSource
    {
        PoTaskStatus GetStatus(short token);
        void         OnCompleted(Action<Action> continuation, object state, short token);
        void         GetResult(short token);
        bool         MoveNext();
    }

    public interface PoTaskSource<out T> : PoTaskSource
    {
        new T GetResult(short token);
    }
}