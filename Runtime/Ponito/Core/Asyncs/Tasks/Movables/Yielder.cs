using System;
using Ponito.Core.Asyncs.Interfaces;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    public class Yielder : Movable, IDisposable
    {
        private Action continuation { get; set; }
        public  bool   IsCompleted  { get; private set; }

        public bool MoveNext()
        {
            // typeof(YieldMovable).F(nameof(MoveNext));
            if (IsCompleted) return false;
            
            continuation?.Invoke(); // Hard logic, has to do continuation first
            IsCompleted = true;     // then mark completion for PoTask
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