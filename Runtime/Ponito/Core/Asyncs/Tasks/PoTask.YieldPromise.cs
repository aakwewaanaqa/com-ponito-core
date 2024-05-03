using System;
using System.Threading;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Extensions;

namespace Ponito.Core.Asyncs.Tasks
{
    public readonly partial struct PoTask
    {
        public static PoTask Yield()
        {
            var source = YieldPromise.Create(PlayerLoopTiming.Update,
                                             default,
                                             false,
                                             out short token);
            return new PoTask(source, token);
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
                    return AutoResetPoTaskCompletionSource.CreateFromCanceled(ct, out token);
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

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    if (!cancelImmediately && !ct.IsCancellationRequested) TryReturn();
                }
            }

            public PoTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (ct.IsCancellationRequested)
                {
                    core.TrySetCanceled(ct);
                    return false;
                }

                core.TrySetResult(null);
                return false;
            }

            bool TryReturn()
            {
                // TaskTracker.RemoveTracking(this);
                core.Reset();
                ct = default;
                ctr.Dispose();
                cancelImmediately = default;
                return pool.TryPush(this);
            }
        }
    }
}