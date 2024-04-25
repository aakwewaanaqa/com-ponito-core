using System;
using System.Collections.Generic;
using Ponito.Core.Asyncronized.Interfaces;
using Ponito.Core.DebugHelper;
using UnityEngine;

namespace Ponito.Core.Asyncronized.Runner
{
    public class PoTaskRunner : MonoSingleton<PoTaskRunner>
    {
        protected override bool isInitialized       => true;
        protected override bool isDontDestroyOnLoad => true;

        protected override void Initialize()
        {
        }

        private readonly Queue<Item> queue = new();

        private struct Item
        {
            [NonSerialized] public Completable awaitable;
            [NonSerialized] public Action    continuation;

            public bool IsCompleted   => awaitable.IsCompleted;
            public void OnCompleted() => awaitable.OnCompleted(continuation);
        }

        public void Enqueue(Completable awaitable, Action continuation)
        {
            queue.Enqueue(new Item
            {
                awaitable    = awaitable,
                continuation = continuation,
            });
        }

        private void Update()
        {
            if (!queue.TryPeek(out var item)) return;
            typeof(PoTaskRunner).F(nameof(Update));
            if (!item.IsCompleted) return;
            item.OnCompleted();
            queue.Dequeue();
        }
    }
}