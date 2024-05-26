using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks.Movables;
using Ponito.Core.Asyncs.Extensions;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks
{
    /// <summary>
    ///     異步任務基礎
    /// </summary>
    public abstract class PoTaskBase
    {
        /// <summary>
        ///     儲存執行時產生的 <see cref="System.Exception"/>
        /// </summary>
        private Exception exception;

        /// <summary>
        ///     儲存內部 <see cref="Movable"/> 
        /// </summary>
        private Movable source;

        /// <summary>
        ///     建構任務
        /// </summary>
        /// <param name="source">任務來源</param>
        protected PoTaskBase(Movable source)
        {
            this.source = source;
        }
    
        /// <summary>
        ///     傳回 <see cref="exception"/> 或 <see cref="Movable"/> 的 <see cref="Movable.Ex"/>,
        ///     手動設置這個值 <see cref="PoTaskBase"/> 可以觸發例外喔。
        /// </summary>
        public Exception Exception
        {
            get
            {
                return source switch
                {
                    null => exception,
                    _    => source.Ex,
                };
            }
            set
            {
                if (source != null) source.Ex = value;
                else exception                       = value;
            }
        }

        /// <summary>
        ///     取得 <see cref="source"/>
        /// </summary>
        protected Movable Source => source;

        /// <summary>
        ///     試著設置 <see cref="source"/> 因為不確定是不是 <see cref="Movable"/>
        ///     不是就當用反射去等待其等待者。
        /// </summary>
        /// <param name="awaiter">等待者</param>
        internal void TrySetSource(INotifyCompletion awaiter)
        {
            source = awaiter as Movable ?? new ReflectAwait(awaiter);
        }

        ~PoTaskBase()
        {
            source = null;
        }
    }
}