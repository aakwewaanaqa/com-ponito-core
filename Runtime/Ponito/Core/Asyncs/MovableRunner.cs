using System;
using System.Collections;
using System.Collections.Generic;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncs
{
    public partial class MovableRunner : MonoSingleton<MovableRunner>
    {
        protected override bool IsInitialized       => true;
        protected override bool IsDontDestroyOnLoad => true;

        protected override void Initialize()
        {
        }

        private List<Movable> movables { get; } = new();

        public void Queue(Movable movable)
        {
            // typeof(MovableRunner).F(nameof(Queue));
            movables.Add(movable);
        }

        private void Update()
        {
            // typeof(MovableRunner).F(nameof(Update));
            for (int i = 0; i < movables.Count; i++)
            {
                AGAIN:

                if (i >= movables.Count) break;
                
                var head = movables[i];
                if (head != null)
                {
                    if (head.MoveNext()) continue;
                }

                movables.RemoveAt(i);
                goto AGAIN;
            }
        }
    }
}