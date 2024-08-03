using System;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Asyncs.Extensions
{
    public static partial class Exts
    {
        /// <summary>
        ///     當 <see cref="t" /> 失敗時執行 <see cref="catcher" />
        /// </summary>
        /// <remarks>
        ///     <see cref="catcher" /> 預設是 <see cref="Debug.LogException(Exception)" />
        /// </remarks>
        public static async PoTask Try(this PoTask t, Action<Exception> catcher = null)
        {
            catcher ??= Debug.LogException;
            try { await t; }
            catch (Exception ex) { catcher(ex); }
        }

        public static async PoTask Retry(
            this Func<PoTask> factory,
            int count = int.MaxValue,
            float delay = 1f,
            Action<Exception> catcher = null)
        {
            catcher ??= Debug.LogException;

            for (var i = 0; i < count; i++)
                try
                {
                    await factory();
                    return;
                }
                catch (Exception ex)
                {
                    catcher(ex);
                    await Controls.Delay(delay);
                }
        }

        public static async PoTask<T> Retry<T>(
            this Func<PoTask<T>> factory,
            int count = int.MaxValue,
            float delay = 1f,
            Action<Exception> catcher = null)
        {
            catcher ??= Debug.LogException;

            for (var i = 0; i < count; i++)
                try { return await factory(); }
                catch (Exception ex)
                {
                    catcher(ex);
                    await Controls.Delay(delay);
                }

            throw new InvalidOperationException(nameof(Retry));
        }

        /// <summary>
        ///     當 <see cref="t" /> 完成時執行 <see cref="factory" /> 產生的 <see cref="PoTask" />
        /// </summary>
        public static async PoTask Chain(this PoTask t, Func<PoTask> factory)
        {
            await t;
            await factory();
        }

        /// <summary>
        ///     當 <see cref="t" /> 完成時執行 <see cref="factory" /> 產生的
        ///     <see cref="PoTask{T}" />
        /// </summary>
        public static async PoTask<T> Chain<T>(this PoTask t, Func<PoTask<T>> factory)
        {
            await t;
            return await factory();
        }
    }
}