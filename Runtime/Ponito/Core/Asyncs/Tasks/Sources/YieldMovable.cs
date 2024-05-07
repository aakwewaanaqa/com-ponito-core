﻿using System;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncs.Tasks.Sources
{
    internal class YieldMovable : Movable
    {
        private bool   yielded;
        private Action continuation;
        
        public bool MoveNext()
        {
            typeof(YieldMovable).F(nameof(MoveNext));
            
            if (!yielded)
            {
                yielded = true;
                return true;
            }

            continuation?.Invoke();
            return false;
        }

        public void OnCompleted(Action continuation)
        {
            typeof(YieldMovable).F(nameof(OnCompleted));
            
            MovableRunner.Instance.Queue(this);
            
            this.continuation = continuation;
        }
    }
}