using System;

namespace Ponito.Core.Promises.Compilations
{
    public interface PoTaskSource
    {
        PoTaskStatus GetStatus(short token);
        void         OnCompleted(Action<object> continuation, object state, short token);
        void         GetResult(short token);
    }

    public interface PoTaskSource<T>
    {
        PoTaskStatus GetStatus(short token);
        void         OnCompleted(Action<object> continuation, object state, short token);
        T            GetResult(short token);
    }
}