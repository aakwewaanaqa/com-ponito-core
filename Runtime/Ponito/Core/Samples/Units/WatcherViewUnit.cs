using System.Threading;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Extensions;
using Ponito.Core.Samples.UI;

namespace Ponito.Core.Samples.Units
{
    /// <summary>
    ///     用於組裝視窗的腳本的單元
    /// </summary>
    public sealed class WatcherViewUnit : StateWatcher
    {
        /// <summary>
        ///     用來取消自身矛盾的動畫狀態，但又能基於外部的取消令牌進行取消
        /// </summary>
        private CancellationTokenSource cts;

        public WatcherViewUnit(OnChange onChange)
        {
            isAsyncStyle  = false;
            this.onChange = onChange;
        }

        public WatcherViewUnit(OnChangeFactory onChangeFactory)
        {
            isAsyncStyle         = true;
            this.onChangeFactory = onChangeFactory;
        }

        private bool            isAsyncStyle    { get; }
        private OnChangeFactory onChangeFactory { get; }
        private OnChange        onChange        { get; }

        public void NotifyChange(Statable state, object args)
        {
            cts = cts.LinkAfterCancel(default, out var ct);
            if (isAsyncStyle) _ = onChangeFactory(state, args, ct).Run();
            else onChange(state, args);
        }
    }
}