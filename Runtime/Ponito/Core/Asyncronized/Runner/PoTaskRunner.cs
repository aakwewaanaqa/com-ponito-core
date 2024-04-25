using System;
using System.Collections.Generic;
using Ponito.Core.Asyncronized.Interfaces;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncronized.Runner
{
    public class PoTaskRunner : MonoSingleton<PoTaskRunner>
    {
        private readonly   List<Item> list = new();
        protected override bool       isInitialized       => true;
        protected override bool       isDontDestroyOnLoad => true;

        private void Update()
        {
            if (list.Count == 0) return;
            
            // typeof(PoTaskRunner).F(nameof(Update));
            
            for (int i = 0; i < list.Count; )
            {
                var item = list[i];
                if (item.awaitable is Runnable runnable)
                {
                    runnable.Run();
                }
                if (!item.IsCompleted)
                {
                    i++;
                    continue;
                }
                
                item.awaitable.GetType().F(nameof(item.OnCompleted));
                list.RemoveAt(i);
                item.OnCompleted();
            }
        }

        protected override void Initialize()
        {
        }

        public void Add(Completable awaitable, Action continuation)
        {
            list.Add(new Item
            {
                awaitable    = awaitable,
                continuation = continuation
            });
        }

        private struct Item
        {
            [NonSerialized] public Completable awaitable;
            [NonSerialized] public Action      continuation;

            public bool IsCompleted => awaitable.IsCompleted;

            public void OnCompleted()
            {
                awaitable.OnCompleted(continuation);
            }
        }
    }
}