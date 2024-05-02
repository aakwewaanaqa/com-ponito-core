using System.Threading;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Extensions;

namespace Ponito.Core.Asyncs.Tasks
{
    public readonly partial struct PoTask
    {
        public static PoTaskSource Yield(out short token)
        {
            return YieldPromise.Create(PlayerLoopTiming.Update,
                                       default,
                                       false,
                                       out token);
        }

        private sealed class YieldPromise : PoTaskSource, PlayerLoopItem, TaskPoolNode<YieldPromise>
        {
            private        YieldPromise                             nextNode;
            private        CancellationToken                        ct;
            private        CancellationTokenRegistration            ctr;
            private        bool                                     cancelImmediately;
            private        PoTaskCompletionSourceCore<YieldPromise> core;
            private static TaskPool<YieldPromise>                   pool;

            public ref YieldPromise NextNode => ref nextNode;

            public static PoTaskSource Create(
                PlayerLoopTiming timing,
                CancellationToken ct,
                bool cancelImmediately,
                out short token)
            {
                if (ct.IsCancellationRequested)
                {
                    // return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                    result = new YieldPromise()
                    {
                        ct                = ct,
                        cancelImmediately = cancelImmediately,
                    };

                if (cancelImmediately && ct.CanBeCanceled)
                {
                    result.ctr = ct.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var promise = (YieldPromise)state;
                        promise.core.TrySetCanceled(promise.ct);
                    }, result);
                }
                
                // TaskTracker.TrackActiveTask(result, 3);
                
                PlayerLoopHelper.AddAction(timing, result);
                
                token = result.core.Version;
                return result;
            }
        }
    }
}