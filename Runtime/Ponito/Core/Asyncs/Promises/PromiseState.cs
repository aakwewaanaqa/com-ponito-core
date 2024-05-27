namespace Ponito.Core.Asyncs.Promises
{
    /// <summary>
    ///     承諾的狀態
    /// </summary>
    public enum PromiseState
    {
        /// <summary>
        ///     承諾正在執行中，要耐心等待
        /// </summary>
        Doing  = 0,
        
        /// <summary>
        ///     承諾已完成可以檢視結果
        /// </summary>
        Done   = 1,
        
        /// <summary>
        ///     承諾已經失敗了。。
        /// </summary>
        Failed = -1,
    }
}