using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks.Movables;
using Ponito.Core.Asyncs.Extensions;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks
{
    public abstract class PoTaskBase
    {
        private Exception exception;
        private Movable   source;

        protected PoTaskBase(Movable source)
        {
            this.source = source;
        }
        
        public Exception Exception
        {
            get
            {
                return source switch
                {
                    null => exception,
                    _    => source.Exception,
                };
            }
            set
            {
                if (source != null) source.Exception = value;
                else exception                       = value;
            }
        }

        public Movable Source => source;
        
        public void TrySetSource(INotifyCompletion awaiter)
        {
            source = awaiter as Movable ?? new ReflectAwait(awaiter);
        }

        ~PoTaskBase()
        {
            source = null;
        }
    }
}