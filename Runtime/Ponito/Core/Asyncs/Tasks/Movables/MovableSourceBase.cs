using System;
using System.Text;
using System.Threading;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.DebugHelper;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    /// <summary>
    ///     The <see cref="Movable"/> aysnc unity for <see cref="MovableRunner"/>
    /// </summary>
    public abstract class MovableBase : Movable
    {
        /// <summary>
        ///     Rejects the execution and marks it as an exception.
        /// </summary>
        public virtual Exception Exception { get; set; }

        /// <summary>
        ///     Stores a token which can be cancelled and track to the source.
        ///     Cancels the execution by throwing <see cref="OperationCanceledException"/>.
        /// </summary>
        protected virtual CancellationToken Ct { get; set; }

        /// <summary>
        ///     Stores the remaining part of the execution
        /// </summary>
        protected Action continuation { get; set; }

        /// <summary>
        ///     Gives <see cref="PoTask.Awaiter"/> to check it is completed or not.
        /// </summary>
        public virtual bool IsCompleted { get; protected set; }

        /// <summary>
        ///     C# compiler automatically calls this function atfter <see cref="GetAwaiter"/>.
        ///     Passed the <see cref="continuation"/> which is the remaining part of the execution
        /// </summary>
        /// <param name="continuation"></param>
        public virtual void OnCompleted(Action continuation)
        {
            if (this.continuation != null)
            {
                Debug.LogError("continuation overflown");
                return;
            }

            this.continuation = continuation;
            MovableRunner.Singleton.Enqueue(this);
        }

        /// <summary>
        ///     C# compiler automatically calls this function.
        ///     Gets the result of <see cref="Movable"/>
        /// </summary>
        public virtual void GetResult()
        {
            if (Exception != null) throw Exception;
            Dispose();
        }

        /// <summary>
        ///     Moves by <see cref="MovableRunner"/>.
        /// </summary>
        /// <returns>keep waiting or not</returns>
        public abstract bool MoveNext();

        /// <summary>
        ///     Dispose <see cref="continuation"/>
        /// </summary>
        public virtual void Dispose()
        {
            continuation = null;
        }

        /// <summary>
        ///     C# compiler automatically calls this function.
        ///     Used by <see cref="PoTaskBuilder"/> to get <see cref="Movable"/>
        /// </summary>
        /// <returns></returns>
        public virtual Movable GetAwaiter()
        {
            return this;
        }

        /// <summary>
        ///     Continues, marks completed and returns false for <see cref="MovableRunner"/>
        /// </summary>
        /// <returns>always false</returns>
        protected bool FinishMoveNext()
        {
            continuation?.Invoke(); // Hard logic, has to do continuation first
            IsCompleted = true;     // then mark completion for PoTask
            return false;
        }

        /// <summary>
        ///     Cancels the execution by throwing <see cref="OperationCanceledException"/>.
        /// </summary>
        /// <param name="ct"></param>
        /// <exception cref="OperationCanceledException"></exception>
        protected void ValidateCancel(CancellationToken ct)
        {
            if (!ct.IsCancellationRequested) return;
            
            var str = new StringBuilder("cancel ")
               .Append(GetType().Name.Colorize(DebugColors.CLASS_COLOR));
            
            throw new OperationCanceledException(str.ToString());
        }
    }

    public abstract class MovableBase<T> : MovableBase, Movable<T>
    {
        /// <summary>
        ///     C# compiler automatically calls this function.
        ///     Gets the result of <see cref="Movable"/>
        /// </summary>
        public new abstract T GetResult();

        /// <summary>
        ///     C# compiler automatically calls this function.
        ///     Used by <see cref="PoTaskBuilder"/> to get <see cref="Movable"/>
        /// </summary>
        /// <returns></returns>
        public new Movable<T> GetAwaiter()
        {
            return this;
        }
    }
}