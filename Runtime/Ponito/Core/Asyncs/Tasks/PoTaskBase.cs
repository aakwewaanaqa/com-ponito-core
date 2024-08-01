using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Interfaces;

namespace Ponito.Core.Asyncs.Tasks
{
    /// <summary>
    ///     異步任務基礎
    /// </summary>
    public abstract class PoTaskBase
    {
        /// <summary>
        ///     儲存內部 <see cref="Movable"/> 
        /// </summary>
        private INotifyCompletion source;

        /// <summary>
        ///     建構任務
        /// </summary>
        /// <param name="source">任務來源</param>
        protected PoTaskBase(INotifyCompletion source)
        {
            this.source = source;
        }

        /// <summary>
        ///     傳回 <see cref="Ex"/> 或 <see cref="Movable"/> 的 <see cref="Movable.Ex"/>,
        ///     手動設置這個值 <see cref="PoTaskBase"/> 可以觸發例外喔。
        /// </summary>
        public Exception Ex { get; set; }

        /// <summary>
        ///     取得 <see cref="source"/>
        /// </summary>
        protected INotifyCompletion Source => source;

        /// <summary>
        ///     試著設置 <see cref="source"/> 因為不確定是不是 <see cref="Movable"/>
        ///     不是就當用反射去等待其等待者。
        /// </summary>
        /// <param name="awaiter">等待者</param>
        internal void SetSource(INotifyCompletion awaiter)
        {
            source = awaiter;
        }

        internal bool TryClearSource(INotifyCompletion awaiter)
        {
            if (!ReferenceEquals(awaiter, source)) return false;
            
            source = null;
            return true;
        }

        ~PoTaskBase()
        {
            source = null;
        }
    }
}