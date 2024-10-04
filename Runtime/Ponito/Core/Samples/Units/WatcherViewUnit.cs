using System;
using System.Threading;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Extensions;
using Ponito.Core.Samples.UI;

namespace Ponito.Core.Samples.Units
{
    /// <summary>
    ///     用於組裝視窗的腳本的單元
    /// </summary>
    public sealed class WatcherViewUnit : StateWatcher, IDisposable
    {
        /// <summary>
        ///     用來取消自身矛盾的動畫狀態，但又能基於外部的取消令牌進行取消
        /// </summary>
        private CancellationTokenSource cts;

        public WatcherViewUnit(OnChange onChange, string name = "")
        {
            isAsyncStyle  = false;
            this.onChange = onChange;
            this.Name     = name;
        }

        public WatcherViewUnit(OnChangeFactory onChangeFactory, string name = "")
        {
            isAsyncStyle         = true;
            this.onChangeFactory = onChangeFactory;
            this.Name            = name;
        }

        private readonly bool            isAsyncStyle;
        private          OnChangeFactory onChangeFactory;
        private          OnChange        onChange;

        public string Name { get; }

        public void NotifyChange(Statable state, object args)
        {
            cts = cts.LinkAfterCancel(default, out var ct);
            if (isAsyncStyle) _ = onChangeFactory(state, args, ct).Run();
            else onChange(state, args);
        }

        public void Dispose()
        {
            cts?.Dispose();
            onChangeFactory = null;
            onChange        = null;
        }
    }
}