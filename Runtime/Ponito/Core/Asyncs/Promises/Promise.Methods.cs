using System;
using System.Collections;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Asyncs.Tasks.Movables;
using UnityEngine;
using UnityEngine.Networking;

namespace Ponito.Core.Asyncs.Promises
{
    public partial class Promise : IDisposable
    {
        /// <summary>
        ///     Runs new <see cref="Action"/>
        /// </summary>
        public Promise Run(Action act)
        {
            ValidateThrow(this);

            var q = new Promise();
            q.caller = Call;
            _        = Call(q);
            return q;

            async PoTask Call(Promise o)
            {
                try
                {
                    act();
                    o.State = PromiseState.Done;
                }
                catch (Exception e)
                {
                    o.Error = e;
                    o.State = PromiseState.Failed;
                }
            }
        }

        /// <summary>
        ///     Runs new <see cref="PoTask"/>
        /// </summary>
        public Promise Run(Func<PoTask> act)
        {
            ValidateThrow(this);

            var q = new Promise();
            q.caller = Call;
            _        = Call(q);
            return q;

            async PoTask Call(Promise o)
            {
                try
                {
                    await act();
                    o.State = PromiseState.Done;
                }
                catch (Exception e)
                {
                    o.Error = e;
                    o.State = PromiseState.Failed;
                }
            }
        }

        /// <summary>
        ///     Runs new <see cref="Coroutine"/>
        /// </summary>
        public Promise Run(IEnumerator coroutine)
        {
            ValidateThrow(this);

            var q = new Promise();
            q.caller = Call;
            _        = Call(q);
            return q;

            async PoTask Call(Promise o)
            {
                try
                {
                    CoroutineRunner.Singleton.Run(coroutine, out var p);
                    await p;
                    o.State = PromiseState.Done;
                }
                catch (Exception e)
                {
                    o.Error = e;
                    o.State = PromiseState.Failed;
                }
            }
        }

        /// <summary>
        ///     Runs new <see cref="UnityWebRequest"/>
        /// </summary>
        public Promise Run(UnityWebRequest req)
        {
            ValidateThrow(this);

            var q = new Promise();
            q.caller = Call;
            _        = Call(q);
            return q;

            async PoTask Call(Promise o)
            {
                try
                {
                    req.SendWebRequest();
                    while (!req.isDone) await PoTask.Yield();
                    if (req.result != UnityWebRequest.Result.Success) throw new Exception(req.error);
                    o.State = PromiseState.Done;
                }
                catch (Exception e)
                {
                    o.Error = e;
                    o.State = PromiseState.Failed;
                }
            }
        }

        /// <summary>
        ///     Repeats last operation in <see cref="count"/> times.
        /// </summary>
        public Promise TryAgain(int count)
        {
            var q = new Promise();
            _ = Call(q);
            return q;

            async PoTask Call(Promise o)
            {
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        await this;
                        if (State is PromiseState.Failed)
                        {
                            State = PromiseState.Doing;
                            caller(this);
                        }
                        else if (State is PromiseState.Done)
                        {
                            break;
                        }
                    }

                    o.State = PromiseState.Done;
                }
                catch (Exception e)
                {
                    o.Error = e;
                    o.State = PromiseState.Failed;
                }
            }
        }

        public void Dispose()
        {
            Error = null;
        }
    }
}