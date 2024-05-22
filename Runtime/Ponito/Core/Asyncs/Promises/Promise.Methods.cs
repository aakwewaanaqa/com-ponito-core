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
            q.caller = () => this.Run(act);
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
            q.caller = () => this.Run(act);
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
        public Promise Run(IEnumerator act)
        {
            ValidateThrow(this);

            var q = new Promise();
            q.caller = () => this.Run(act);
            _        = Call(q);
            return q;

            async PoTask Call(Promise o)
            {
                try
                {
                    CoroutineRunner.Singleton.Run(act, out var p);
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
        public Promise Run(UnityWebRequest act)
        {
            ValidateThrow(this);

            var q = new Promise();
            q.caller = () => this.Run(act);
            _        = Call(q);
            return q;

            async PoTask Call(Promise o)
            {
                try
                {
                    act.SendWebRequest();
                    while (!act.isDone) await PoTask.Yield();
                    if (act.result != UnityWebRequest.Result.Success) throw new Exception(act.error);
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
        public Promise TryAgain(int count = int.MaxValue, Action<Exception> catcher = null)
        {
            var q = new Promise();
            q.caller = () => this.TryAgain(count);
            _        = Call(q);
            return q;

            async PoTask Call(Promise output)
            {
                try
                {
                    var p = this;
                    
                    for (int i = 0; i < count; i++)
                    {
                        while (p.IsDoing) await PoTask.Yield();
                        
                        if (p.State is PromiseState.Failed)
                        {
                            if (p.Error is Exception ex) catcher?.Invoke(ex);
                            
                            p.State = PromiseState.Doing;
                            p       = caller();
                        }
                        else if (p.State is PromiseState.Done)
                        {
                            break;
                        }
                    }

                    output.State = PromiseState.Done;
                }
                catch (Exception e)
                {
                    output.Error = e;
                    output.State = PromiseState.Failed;
                }
            }
        }

        public void Dispose()
        {
            Error = null;
        }
    }
}