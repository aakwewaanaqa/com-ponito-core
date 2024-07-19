using System.Threading;
using Ponito.Core.Asyncs.Tasks;

namespace Ponito.Core.Samples.UI
{
    /// <summary>
    ///     如果你不介意異步顯示、管理、切換顯示介面，可以繼承這個介面
    /// </summary>
    /// <seealso cref="Show"/>
    /// <seealso cref="Hide"/>
    public interface PoTaskView
    {
        /// <summary>
        ///     異步地顯示 <see cref="PoTaskView"/>
        /// </summary>
        /// <param name="args">任意自訂參數</param>
        /// <param name="ct">萬一過程需要取消的鏢旗</param>
        PoTask Show(object args, CancellationToken ct = default);

        /// <summary>
        ///     異步地隱藏 <see cref="PoTaskView"/>
        /// </summary>
        /// <param name="args">任意自訂參數</param>
        /// <param name="ct">萬一過程需要取消的鏢旗</param>
        PoTask Hide(object args, CancellationToken ct = default);
        
        /// <summary>
        ///     取得 <see cref="PoTaskView"/> 的狀態
        /// </summary>
        ViewState State { get; }
    }
}