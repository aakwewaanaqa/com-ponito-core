using System;
using System.Collections;
using System.Diagnostics;
using Ponito.Core.Asyncs.Promises;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks
{
    public partial class PoTask<T>
    {
        [DebuggerHidden]
        public IEnumerator RunAsCoroutine(Promise<T> promise)
        {
            promise.State = PromiseState.Doing;
            Awaiter awaiter = GetAwaiter();
            MovableRunner.Singleton.Enqueue(awaiter);
            while (!awaiter.IsCompleted) yield return new WaitForEndOfFrame();
            try
            {
                if (awaiter.Exception != null) throw awaiter.Exception;
                promise.Result = awaiter.GetResult();
                promise.State  = PromiseState.Done;
            }
            catch (Exception e)
            {
                promise.Error = e;
                promise.State = PromiseState.Failed;
            }
        }
    }
}