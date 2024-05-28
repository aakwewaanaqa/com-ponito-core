using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Promises;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Asyncs.Tasks.Movables;
using Ponito.Core.DebugHelper;
using UnityEngine;

namespace Ponito.Core.Asyncs
{
    /// <summary>
    ///     運作 <see cref="Movable.MoveNext()"/> 的核心
    /// </summary>
    public class MovableRunner : MonoSingleton<MovableRunner>
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
            task.name = name;
            waitings.Enqueue(new RunnerItem(task, source));
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
            for (var i = 0; i < items.Length; i++)
            {
                if (items[i] != null) continue;

                // TODO: Tack movable
                items[i] = source;
                return;
            }

            Array.Resize(ref items, items.Length * 2);
            AddToMovables(source);
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

        private class RunnerItem : IDisposable
        {
            private PoTask            task     { get; }
            private INotifyCompletion source   { get; }
            private Func<bool>        moveNext { get; set; }

            public Exception Ex
            {
                get => task?.Ex;
                set
                {
                    if (task != null) task.Ex = value;
                }
            }

            public RunnerItem(PoTask task, INotifyCompletion source)
            {
                this.task   = task;
                this.source = source;

                task.SetSource(source);

                if (source is Movable movable)
                {
                    moveNext = () => movable.MoveNext();
                    return;
                }

                ("使用反射等待 " + source.GetType().Name).Warn();
                var property = source.GetType().GetProperty("IsCompleted");
                if (property != null)
                {
                    moveNext = () => !(bool)property.GetValue(source);
                    return;
                }

                moveNext = () => true;
            }

            public bool TryMoveNext()
            {
                return moveNext();
            }

            public void Dispose()
            {
                if (source is IDisposable disposable) disposable.Dispose();
                task?.TryClearSource(source);
            }
        }
    }
}