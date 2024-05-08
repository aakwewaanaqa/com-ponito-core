using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.DebugHelper;

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

                movables[i] = movable;
                return;
            }

            Array.Resize(ref movables, movables.Length * 2);
            AddToMovables(movable);
        }

        private void Update()
        {
            // typeof(MovableRunner).F(nameof(Update));
            for (int i = 0; i < movables.Length; i++)
            {
                var head = movables[i];
                if (head == null) continue;
                if (!head.MoveNext()) movables[i] = null;
            }

            while (queue.TryDequeue(out var movable)) AddToMovables(movable);
        }
    }
}