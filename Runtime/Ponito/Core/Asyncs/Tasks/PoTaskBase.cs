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
        /// <summary>
        ///     Stores exception to this <see cref="PoTaskBase"/> 
        /// </summary>
        private Exception exception;

        /// <summary>
        ///     Stores source to this <see cref="PoTaskBase"/> 
        /// </summary>
        private Movable source;

        protected PoTaskBase(Movable source)
        {
            this.source = source;
        }
    
        /// <summary>
        ///     <see cref="exception"/> or another <see cref="Movable"/>'s <see cref="Movable.Exception"/>,
        ///     manually setting this will cancel the whole <see cref="PoTaskBase"/>.
        /// </summary>
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

        /// <summary>
        ///     The <see cref="source"/>
        /// </summary>
        protected Movable Source => source;

        /// <summary>
        ///     Sets the source to await, except, continue.
        /// </summary>
        /// <param name="awaiter"></param>
        internal void TrySetSource(INotifyCompletion awaiter)
        {
            source = awaiter as Movable ?? new ReflectAwait(awaiter);
        }

        ~PoTaskBase()
        {
            source = null;
        }
    }
}