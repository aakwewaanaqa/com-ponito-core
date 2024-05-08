using System;
using Ponito.Core.Asyncs.Interfaces;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    public class Yielder : Movable, IDisposable
    {
        private int    frame;
        private Action continuation;

        public bool IsCompleted => frame == 2;

        public bool MoveNext()
        {
            // typeof(YieldMovable).F(nameof(MoveNext));
            if (frame == 0)
            {
                frame = 1;
                continuation?.Invoke();
                frame = 2;
                return false;
            }

            return false;
        }

        public void OnCompleted(Action continuation)
        {
            // typeof(YieldMovable).F(nameof(OnCompleted));
            if (this.continuation != null)
            {
                Debug.LogError("continuation overflown");
                return;
            }

            this.continuation = continuation;
            MovableRunner.Instance.AddToQueue(this);
        }

        public Movable GetAwaiter() => this;

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