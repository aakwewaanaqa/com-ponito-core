using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncs
{
    /// <summary>
    ///     運作 <see cref="Movable.MoveNext()"/> 的核心
    /// </summary>
    public partial class MovableRunner : MonoSingleton<MovableRunner>
    {
        /// <summary>
        ///     起始 <see cref="items"/> 的數量
        /// </summary>
        private const int INITIAL_CAPACITY = 16;

        /// <summary>
        ///     所有 <see cref="Movable"/> 在這裡等直到下一個 <see cref="MovableRunner.Update()"/>
        /// </summary>
        private readonly Queue<RunnerItem> waitings = new();

        /// <summary>
        ///     所有 <see cref="RunnerItem"/> 會在這個陣列中暫時儲存，
        ///     當 <see cref="RunnerItem"/> 完成是會把其位置標註為 null
        /// </summary>
        private RunnerItem[] items = new RunnerItem[INITIAL_CAPACITY];

        /// <inheritdoc />
        protected override bool IsInitialized => true;

        /// <inheritdoc />
        protected override bool IsDontDestroyOnLoad => true;

        /// <summary>
        ///     取得所有 <see cref="RunnerItem"/>
        /// </summary>
        public Span<RunnerItem> Items => items;
        
        /// <inheritdoc />
        protected override void Initialize()
        {
        }

        /// <summary>
        ///     先把 <see cref="RunnerItem"/> 加入 <see cref="waitings"/> 後等待
        /// </summary>
        /// <param name="task">要加入的任務</param>
        /// <param name="source">要加入的等待來源</param>
        /// <param name="name"><see cref="PoTask"/>的名字</param>
        public void AwaitSource(PoTask task, INotifyCompletion source, object name)
        {
            waitings.Enqueue(new RunnerItem(task, source, name));
        }

        /// <summary>
        ///     執行 <see cref="AddToMovables"/> 後 <see cref="RunItem"/>
        /// </summary>
        private void Update()
        {
            while (waitings.TryDequeue(out var movable)) AddToMovables(movable);
            for (var i = 0; i < items.Length; i++) RunItem(i);
        }

        /// <summary>
        ///     將 <see cref="Movable"/> 從 <see cref="waitings"/> 加入 <see cref="items"/>
        /// </summary>
        /// <param name="source">要真正加入的等待來源</param>
        private void AddToMovables(RunnerItem source)
        {
            while (true)
            {
                for (var i = 0; i < items.Length; i++)
                {
                    if (items[i] != null) continue;

                    // TODO: Tack movable
                    items[i] = source;
                    return;
                }

                Array.Resize(ref items, items.Length * 2);
            }
        }

        /// <summary>
        ///     推移所有 <see cref="Movable"/> 完成後標註其位置為 null
        /// </summary>
        /// <param name="i">其 <see cref="items"/> 在中的位置</param>
        private void RunItem(int i)
        {
            var head = items[i];
            if (head == null) return;
            try
            {
                if (head.TryMoveNext()) return;
                items[i] = null;
                // TODO: Untrack movable
                head.Dispose();
            }
            catch (Exception e)
            {
                items[i] = null;
                // TODO: Untrack movable
                head.Dispose();
                head.Ex = e;
            }
        }
    }
}