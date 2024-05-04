using System;
using System.Threading;
using Ponito.Core.Asyncs.Compilations;

namespace Ponito.Core.Asyncs.Tasks.Sources
{
    public class AutoResetPoTaskCompletionSource : PoTaskSource,
                                                   TaskPoolNode<AutoResetPoTaskCompletionSource>
    {
        private static TaskPool<AutoResetPoTaskCompletionSource> pool;
        private        AutoResetPoTaskCompletionSource           nextNode;
        private        PoTaskCompletionSourceCore<AsyncUnit>     core;
        private        short                                     version;
        
        public ref AutoResetPoTaskCompletionSource NextNode => ref nextNode;

        public static AutoResetPoTaskCompletionSource Create()
        {
            if (!pool.TryPop(out var result))
            {
                result = new AutoResetPoTaskCompletionSource();
            }
            result.version = result.core.Version;
            // TODO: Tracks source
            // TaskTracker.TrackActiveTask(result, 2);
            return result;
        }
        
        public static AutoResetPoTaskCompletionSource CreateFromCanceled(CancellationToken ct, out short token)
        {
            var source = Create();
            source.TrySetCanceled(ct);
            token = source.core.Version;
            return source;
        }

        public PoTaskStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }

        public void GetResult(short token)
        {
            try
            {
                core.GetResult(token);
            }
            finally
            {
                TryReturn();
            }
        }

        public bool TrySetCanceled(CancellationToken ct = default)
        {
            return version == core.Version && core.TrySetCanceled(ct);
        }

        private bool TryReturn()
        {
            // TODO: Tracks source
            // TaskTracker.RemoveTracking(this);
            core.Reset();
            return pool.TryPush(this);
        }
    }
}