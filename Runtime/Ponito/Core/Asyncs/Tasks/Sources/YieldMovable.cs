using System;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.DebugHelper;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks.Sources
{
    public class YieldMovable : Movable
    {
        private int    yielded;
        private Action continuation;

        public bool   IsCompleted  => yielded == 2;
        public Action Continuation => continuation;

        public bool MoveNext()
        {
            // typeof(YieldMovable).F(nameof(MoveNext));
            if (yielded == 0)
            {
                yielded = 1;
                return true;
            }

            continuation?.Invoke();
            yielded = 2;
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
            MovableRunner.Instance.Queue(this);
        }

        public Movable GetAwaiter() => this;

        public void GetResult()
        {
            
        }
    }
}