using System;
using System.Collections;
using System.Collections.Generic;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncs
{
    public class MovableRunner : MonoSingleton<MovableRunner>
    {
        protected override bool IsInitialized       => true;
        protected override bool IsDontDestroyOnLoad => true;

        protected override void Initialize()
        {
        }

        private List<Movable> list { get; } = new();

        public void Queue(Movable movable)
        {
            typeof(MovableRunner).F(nameof(Queue));
            list.Add(movable);
        }

        private void Update()
        {
            for (int i = 0; i < list.Count; i++)
            {
                AGAIN:

                if (i >= list.Count) break;
                
                var head = list[i];
                if (head != null)
                {
                    if (head.MoveNext()) continue;
                }

                list.RemoveAt(i);
                goto AGAIN;
            }
        }
    }
}