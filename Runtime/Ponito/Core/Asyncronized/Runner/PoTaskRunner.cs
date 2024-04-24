using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Ponito.Core.Asyncronized
{
    public class PoTaskRunner : MonoSingleton<PoTaskRunner>
    {
        protected override bool isInitialized       => true;
        protected override bool isDontDestroyOnLoad => true;
        protected override void Initialize()
        {
        }

        private readonly Queue<Awaiter> queue = new ();
        
        public void Enqueue(Awaiter item)
        {
            queue.Enqueue(item);
        }

        private void Update()
        {
            if (!queue.TryPeek(out var item)) return;
            if (!item.IsCompleted) return;
            queue.Dequeue();
        }
    }
}