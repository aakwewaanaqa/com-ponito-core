using System;
using Ponito.Core.Asyncs.Tasks;

namespace Ponito.Core.Asyncs.Promises
{
    public partial class Promise<T>
    {
        /// <summary>
        ///     再試一次之前錯誤的承諾動作，並回傳 <see cref="T" />
        /// </summary>
        public new Promise<T> TryAgain(
            int count = int.MaxValue,
            Action<Exception> catcher = null)
        {
            _ = CallAsync(this, async () =>
            {
                var running = this;
                for (var i = 0; i < count; i++)
                {
                    while (running.IsDoing) await PoTask.Yield();

                    if (running.State is PromiseState.Failed)
                    {
                        if (running.Ex is Exception ex) catcher?.Invoke(ex);

                        running.State = PromiseState.Doing;
                        running       = running.Factory();
                    }
                    else if (running.State is PromiseState.Done)
                    {
                        break;
                    }
                }

                return running.Result;
            });

            return this;
        }
    }
}