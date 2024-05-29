using System;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Asyncs.Extensions
{
    public static partial class Exts
    {
        public static async PoTask Retry(this Func<PoTask> factory,
                                         int count = int.MaxValue,
                                         float delay = 1f,
                                         Action<Exception> catcher = null)
        {
            catcher ??= Debug.LogException;
            
            for (var i = 0; i < count; i++)
            {
                try
                {
                    await factory();
                    return;
                }
                catch (Exception ex)
                {
                    catcher(ex);
                    await PoTask.Delay(delay);
                }
            }
        }
        
        public static async PoTask<T> Retry<T>(this Func<PoTask<T>> factory,
                                         int count = int.MaxValue,
                                         float delay = 1f,
                                         Action<Exception> catcher = null)
        {
            catcher ??= Debug.LogException;
            
            for (var i = 0; i < count; i++)
            {
                try
                {
                    return await factory();
                }
                catch (Exception ex)
                {
                    catcher(ex);
                    await PoTask.Delay(delay);
                }
            }

            throw new InvalidOperationException(nameof(Retry));
        }
        
        public static async PoTask Try(this PoTask t, Action<Exception> catcher = null)
        {
            catcher ??= Debug.LogException;
            try
            {
                await t;
            }
            catch (Exception ex)
            {
                catcher(ex);
            }
        }
    }
}