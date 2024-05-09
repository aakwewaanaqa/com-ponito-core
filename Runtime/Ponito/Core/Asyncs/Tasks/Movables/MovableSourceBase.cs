using System;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    /// <summary>
    ///     The <see cref="Movable"/> aysnc unity for <see cref="MovableRunner"/>
    /// </summary>
    public abstract class MovableBase : Movable
    {
        /// <summary>
        ///     Stores the remaining part of the execution
        /// </summary>
        protected Action continuation { get; private set; }

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
            MovableRunner.Instance.AddToQueue(this);
        }

        /// <summary>
        ///     C# compiler automatically calls this function.
        ///     Gets the result of <see cref="Movable"/>
        /// </summary>
        public virtual void GetResult()
        {
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
    }

    public abstract class MovableBase<T> : Movable<T>
    {
        /// <summary>
        ///     Stores the remaining part of the execution
        /// </summary>
        protected Action continuation { get; private set; }

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
            MovableRunner.Instance.AddToQueue(this);
        }

        /// <summary>
        ///     C# compiler automatically calls this function.
        ///     Gets the result of <see cref="Movable"/>
        /// </summary>
        void Movable.GetResult()
        {
            Dispose();
        }

        /// <summary>
        ///     C# compiler automatically calls this function.
        ///     Gets the result of <see cref="Movable"/>
        /// </summary>
        public abstract T GetResult();

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
    }
}