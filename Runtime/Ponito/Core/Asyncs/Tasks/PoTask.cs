using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public partial class PoTask
    {
        internal Movable source;

        public PoTask(Movable source = null)
        {
            this.source = source;
        }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        public class Awaiter : Movable, IDisposable
        {
            public Awaiter(in PoTask task)
            {
                this.task    = task;
                continuation = null;
            }

            internal PoTask task         { get; }
            private  Action continuation { get; set; }

            public void OnCompleted(Action continuation)
            {
                if (this.continuation != null)
                {
                    Debug.LogError("continuation overflown");
                    return;
                }

                this.continuation = continuation;
                MovableRunner.Instance.AddToQueue(this);
            }

            public bool IsCompleted => task.source?.IsCompleted ?? true;

            public bool MoveNext()
            {
                if (!IsCompleted) return true;

                continuation?.Invoke();
                return false;
            }

            public void GetResult()
            {
                Dispose();
            }

            public void Dispose()
            {
                continuation = null;
            }
        }
    }

    [AsyncMethodBuilder(typeof(PoTaskBuilder<>))]
    public readonly struct PoTask<T>
    {
        private readonly short   token;
        private readonly Movable source;

        public PoTask(Movable source, short token)
        {
            this.source = source;
            this.token  = token;
        }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        public struct Awaiter : INotifyCompletion
        {
            private readonly PoTask<T> task;
            private          Action    continuation;

            public Awaiter(PoTask<T> task)
            {
                this.task    = task;
                continuation = null;
            }

            public bool IsCompleted => false;

            public T GetResult()
            {
                return default;
            }

            public void OnCompleted(Action continuation)
            {
                this.continuation = continuation;
            }

            public bool MoveNext()
            {
                if (task.source is null)
                {
                    continuation();
                    return false;
                }

                return task.source.MoveNext();
            }
        }
    }
}