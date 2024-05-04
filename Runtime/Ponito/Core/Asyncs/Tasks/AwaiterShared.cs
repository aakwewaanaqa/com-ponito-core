using System;

namespace Ponito.Core.Asyncs.Tasks
{
    internal static class AwaiterShared
    {
        public static readonly Action<object> ContinuationDelegate = OnCompleted;

        public static void OnCompleted(object state)
        {
            (state as Action)?.Invoke();
        }
    }
}