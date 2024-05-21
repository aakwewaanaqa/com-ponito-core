using System;
using System.Collections.Generic;
using Ponito.Core.Asyncs.Interfaces;
using UnityEngine;

namespace Ponito.Core.Asyncs
{
    public class MovableRunner : MonoSingleton<MovableRunner>
    {
        /// <summary>
        ///     The initial capacity of <see cref="movables"/>.
        /// </summary>
        private const int INITIAL_CAPACITY = 16;

        /// <summary>
        ///     All the <see cref="Movable"/> waits here until next <see cref="MovableRunner.Update()"/>
        /// </summary>
        private readonly Queue<Movable> waitings = new();

        /// <summary>
        ///     All the <see cref="Movable"/> running in this array.
        ///     When <see cref="Movable"/> is completed, it will be set as null.
        /// </summary>
        private Movable[] movables = new Movable[INITIAL_CAPACITY];

        /// <inheritdoc />
        protected override bool IsInitialized => true;

        /// <inheritdoc />
        protected override bool IsDontDestroyOnLoad => true;

        /// <inheritdoc />
        protected override void Initialize()
        {
        }

        /// <summary>
        ///     Enqueues the <see cref="Movable"/> in this frame.
        /// </summary>
        /// <param name="movable"></param>
        public void Enqueue(Movable movable)
        {
            waitings.Enqueue(movable);
        }

        /// <summary>
        ///     Runs <see cref="AddToMovables"/>
        ///     then <see cref="RunItems"/>.
        /// </summary>
        private void Update()
        {
            while (waitings.TryDequeue(out var movable)) AddToMovables(movable);
            for (var i = 0; i < movables.Length; i++) RunItems(i);
        }

        /// <summary>
        ///     Adds <see cref="Movable"/> to <see cref="movables"/>
        /// </summary>
        /// <param name="movable"></param>
        private void AddToMovables(Movable movable)
        {
            for (var i = 0; i < movables.Length; i++)
            {
                if (movables[i] != null) continue;

                // TODO: Tack movable
                movables[i] = movable;
                return;
            }

            Array.Resize(ref movables, movables.Length * 2);
            AddToMovables(movable);
        }

        /// <summary>
        ///     Runs every <see cref="Movable"/> when finished,
        ///     marks it null.
        /// </summary>
        /// <param name="i"></param>
        private void RunItems(int i)
        {
            var head = movables[i];
            if (movables[i] == null) return;
            try
            {
                if (!head.MoveNext())
                    // TODO: Untrack movable
                    movables[i] = null;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                // TODO: Untrack movable
                movables[i] = null;
            }
        }
    }
}