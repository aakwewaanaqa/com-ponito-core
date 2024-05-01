using System;

namespace CompilerServices
{
    public struct CompletionSourceCore<T>
    {
        // Struct Size: TResult + (8 + 2 + 1 + 1 + 8 + 8)

        private T              result;
        private object         error; // ExceptionHolder or OperationCanceledException
        private short          version;
        private bool           hasUnhandledError;
        private int            completedCount; // 0: completed == false
        private Action<object> continuation;
        private object         continuationState;

        public void Reset()
        {
            unchecked
            {
                version += 1;
            }

            completedCount    = 0;
            result            = default;
            error             = null;
            hasUnhandledError = false;
            continuation      = null;
            continuationState = null;
        }

        public bool TrySetResult(T result)
        {
            
        }
    }
}