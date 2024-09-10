using System.Threading;

namespace Ponito.Core.Extensions
{
    public static class CancellationTokenSourceExts
    {
        /// <summary>
        ///     取消前一次<see cref="cts"/>之後對內部的<see cref="cts"/>向外部<see cref="outer"/>
        ///     鏈結，如果<see cref="outer"/>被取消了，<see cref="cts"/>也會被取消
        /// </summary>
        /// <param name="cts">內部令牌來源</param>
        /// <param name="outer">外部令牌</param>
        /// <param name="ct">新的令牌</param>
        /// <returns>新的內部令牌來源</returns>
        public static CancellationTokenSource LinkAfterCancel(this CancellationTokenSource cts, CancellationToken outer, out CancellationToken ct)
        {
            cts?.Cancel();
            cts = CancellationTokenSource.CreateLinkedTokenSource(outer);
            ct = cts.Token;
            return cts;
        }
    }
}