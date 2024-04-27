using System;
using Ponito.Core.Threading.Enums;

namespace Ponito.Core.Threading.Interfaces
{
    public interface PTaskSource
    {
        PTaskStatus GetStatus();
        void        OnCompleted(Action<object> continuation, object state);
        void        GetResult();
    }
}