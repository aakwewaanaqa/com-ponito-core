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
        internal  Exception ex;
        protected Movable   source;

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