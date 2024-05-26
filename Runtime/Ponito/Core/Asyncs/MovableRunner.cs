using System;
using System.Collections;
using System.Collections.Generic;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Promises;
using UnityEngine;

namespace Ponito.Core.Asyncs
{
    /// <summary>
    ///     運作 <see cref="Movable.MoveNext()"/> 的核心
    /// </summary>
    public class MovableRunner : MonoSingleton<MovableRunner>
    {
        /// <summary>
        ///     起始 <see cref="movables"/> 的數量
        /// </summary>
        private const int INITIAL_CAPACITY = 16;

        /// <summary>
        ///     所有 <see cref="Movable"/> 在這裡等直到下一個 <see cref="MovableRunner.Update()"/>
        /// </summary>
        private readonly Queue<Movable> waitings = new();

        /// <summary>
        ///     所有 <see cref="Movable"/> 會在這個陣列中暫時儲存，
        ///     當 <see cref="Movable"/> 完成是會把其位置標註為 null
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
        ///     先把 <see cref="Movable"/> 加入 <see cref="waitings"/> 後等待
        /// </summary>
        /// <param name="source">要加入的等待來源</param>
        public void Enqueue(Movable source)
        {
            waitings.Enqueue(source);
        }

        /// <summary>
        ///     執行 <see cref="AddToMovables"/> 後 <see cref="RunItem"/>
        /// </summary>
        private void Update()
        {
            while (waitings.TryDequeue(out var movable)) AddToMovables(movable);
            for (var i = 0; i < movables.Length; i++) RunItem(i);
        }

        /// <summary>
        ///     將 <see cref="Movable"/> 從 <see cref="waitings"/> 加入 <see cref="movables"/>
        /// </summary>
        /// <param name="source">要真正加入的等待來源</param>
        private void AddToMovables(Movable source)
        {
            for (var i = 0; i < movables.Length; i++)
            {
                if (movables[i] != null) continue;

                // TODO: Tack movable
                movables[i] = source;
                return;
            }

            Array.Resize(ref movables, movables.Length * 2);
            AddToMovables(source);
        }

        /// <summary>
        ///     推移所有 <see cref="Movable"/> 完成後標註其位置為 null
        /// </summary>
        /// <param name="i">其 <see cref="movables"/> 在中的位置</param>
        private void RunItem(int i)
        {
            var head = movables[i];
            if (head == null) return;
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
                // TODO: Untrack movable
                head.Ex = e;
                // Debug.LogException(e);
                movables[i] = null;
            }
        }
    }
}