﻿using System;
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
        ///     偵測錯誤擲回 <see cref="PromiseException.TryThrow()"/> ，製作一個承諾，並設置 <see cref="factory"/>
        /// </summary>
        /// <param name="factory">如果失敗之後再做一次的方法</param>
        /// <param name="p">回傳承諾</param>
        /// <returns>此行為承諾</returns>
        private Promise Create(Func<Promise> factory, out Promise p)
        {
            // 在下一步開始之前，先檢測上一個承諾是否有錯誤
            Ex?.TryThrow();

            // 給予新的動作一個承諾
            p         = new Promise();
            p.factory = factory;
            return p;
        }

        /// <summary>
        ///     異步地執行這個承諾的內涵
        /// </summary>
        /// <param name="p">此行為承諾</param>
        /// <param name="factory">異步任務</param>
        private static async PoTask CallAsync(Promise p, Func<PoTask> factory)
        {
            try
            {
                await factory();
                p.State = PromiseState.Done;
            }
            catch (PromiseException e)
            {
                p.Ex    = e;
                p.State = PromiseState.Failed;
            }
            catch (Exception e)
            {
                p.Ex    = new PromiseException(e);
                p.State = PromiseState.Failed;
            }
        }

        /// <summary>
        ///     執行一個同步動作
        /// </summary>
        public Promise Run(Action act)
        {
            _ = CallAsync(
                Create(() => Run(act), out var p),
                async () => act());
            return p;
        }

        /// <summary>
        ///     執行一個同異步動作
        /// </summary>
        public Promise Run(Func<PoTask> act)
        {
            _ = CallAsync(
                Create(() => Run(act), out var p),
                async () => await act());
            return p;
        }

        /// <summary>
        ///     執行一個協程動作 <see cref="Coroutine"/>
        /// </summary>
        public Promise Run(IEnumerator act)
        {
            _ = CallAsync(
                Create(() => Run(act), out var p),
                async () =>
                {
                    CoroutineRunner.Singleton.Run(act, out var another);
                    await another;
                });
            return p;
        }

        /// <summary>
        ///     執行一個網路協議 <see cref="UnityWebRequest"/>
        /// </summary>
        public Promise Run(UnityWebRequest act)
        {
            _ = CallAsync(
                Create(() => Run(act), out var p),
                async () =>
                {
                    act.SendWebRequest();
                    while (!act.isDone) await PoTask.Yield();
                    var reponse = new UnityWebResponse
                    {
                        result      = act.result,
                        reponseCode = act.responseCode,
                        message     = act.error,
                    };
                    if (act.result != UnityWebRequest.Result.Success) throw new PromiseException(reponse);
                });
            return p;
        }

        /// <summary>
        ///     再試一次之前錯誤的承諾動作
        /// </summary>
        public Promise TryAgain(int count = int.MaxValue, Action<Exception> catcher = null)
        {
            _ = CallAsync(this, async () =>
            {
                var running = this;
                for (int i = 0; i < count; i++)
                {
                    while (running.IsDoing) await PoTask.Yield();

                    if (running.State is PromiseState.Failed)
                    {
                        if (running.Ex is Exception ex) catcher?.Invoke(ex);

                        running.State = PromiseState.Doing;
                        running       = running.factory();
                    }
                    else if (running.State is PromiseState.Done)
                    {
                        break;
                    }
                }
            });
            return this;
        }

        public void Dispose()
        {
            Ex      = null;
            factory = null;
        }
    }
}