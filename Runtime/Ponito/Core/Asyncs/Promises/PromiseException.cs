using System;

namespace Ponito.Core.Asyncs.Promises
{
    /// <summary>
    ///     多元儲存例外狀態，利用 <see cref="PromiseException(object)"/> 可以存取你要的資訊
    /// </summary>
    public class PromiseException : Exception
    {
        /// <summary>
        ///     儲存客製化的錯誤資訊
        /// </summary>
        public object Error { get; }

        /// <summary>
        ///     把客製化的錯誤資訊存入 <see cref="msg"/> 如果是個 <see cref="System.Exception"/>
        ///     可以被 <see cref="TryThrow"/> 直接擲回，或是擲回 <see cref="PromiseException"/>
        /// </summary>
        /// <param name="msg">客製化的錯誤資訊</param>
        public PromiseException(object msg) : base(msg?.ToString())
        {
            Error = msg;
        }

        /// <summary>
        ///     <see cref="Error"/> 如果是個 <see cref="System.Exception"/>
        ///     可以被 <see cref="TryThrow"/> 直接擲回，或是擲回 <see cref="PromiseException"/>
        /// </summary>
        /// <exception cref="Exception">擲回的錯誤</exception>
        public void TryThrow()
        {
            if (Error == null) return;
            if (Error is Exception ex) throw ex;
            throw this;
        }
    }
}