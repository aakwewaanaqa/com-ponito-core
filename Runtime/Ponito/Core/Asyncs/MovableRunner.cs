using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.DebugHelper;
using UnityEngine;

namespace Ponito.Core.Asyncs
{
    public partial class MovableRunner : MonoSingleton<MovableRunner>
    {
        private const int INITIAL_CAPACITY = 16;

        protected override bool IsInitialized       => true;
        protected override bool IsDontDestroyOnLoad => true;

        protected override void Initialize()
        {
        }

        private          Movable[]      movables = new Movable[INITIAL_CAPACITY];
        private readonly Queue<Movable> queue    = new();

        public void AddToQueue(Movable movable)
        {
            queue.Enqueue(movable);
        }

        private void AddToMovables(Movable movable)
        {
            for (int i = 0; i < movables.Length; i++)
            {
                if (movables[i] != null) continue;

                // TODO: Tack movable
                movables[i] = movable;
                return;
            }

            Array.Resize(ref movables, movables.Length * 2);
            AddToMovables(movable);
        }

        private void Update()
        {
            for (int i = 0; i < movables.Length; i++) RunItem(i);
            while (queue.TryDequeue(out var movable)) AddToMovables(movable);
        }

        private void RunItem(int i)
        {
            var head = movables[i];
            if (movables[i] == null) return;
            try
            {
                if (!head.MoveNext())
                {
                    // TODO: Untrack movable
                    movables[i] = null;
                }
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