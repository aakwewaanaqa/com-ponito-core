using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks
{
    /// <summary>
    ///     Borrowed from UniTask
    ///     https://github.com/Cysharp/UniTask/blob/master/src/UniTask/Assets/Plugins/UniTask/Runtime/Internal/PlayerLoopRunner.cs
    /// </summary>
    public partial class PlayerLoopRunner : MonoSingleton<PlayerLoopRunner>
    {
        private const int SIZE = 16;

        private readonly object            lockRunOrQueue = new();
        private readonly object            lockArray      = new();
        private readonly Queue<PlayerLoopItem> waits          = new(SIZE);
        private          PlayerLoopItem[]      items          = new PlayerLoopItem[SIZE];

        private bool isRunning { get; set; } = false;
        private int  tail      { get; set; } = 0;

        internal void Add(PlayerLoopItem item)
        {
            lock (lockRunOrQueue)
            {
                if (isRunning)
                {
                    waits.Enqueue(item);
                    return;
                }
            }

            lock (lockArray)
            {
                if (tail == items.Length) Array.Resize(ref items, checked(tail * 2));
                items[++tail] = item;
            }
        }

        internal  int Clear()
        {
            lock (lockArray)
            {
                var rest = 0;

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null) rest += 1;
                    items[i] = null;
                }

                tail = 0;
                return rest;
            }
        }

        /// <summary>
        ///     Seems to run in front doing <see cref="LoopItem.MoveNext()"/>,
        ///     if finished as false <see cref="LoopItem"/> found,
        ///     goes to tail to get not finished <see cref="LoopItem"/> and put it back to front. 
        /// </summary>
        private void Run()
        {
            lock (lockRunOrQueue)
            {
                isRunning = true;
            }

            lock (lockArray)
            {
                var j = tail - 1;

                for (var i = 0; i < items.Length; i++)
                {
                    var fromHead = items[i];
                    if (fromHead != null)
                    {
                        try
                        {
                            if (fromHead.MoveNext())
                            {
                                goto NEXT_I;
                            }
                            else
                            {
                                items[i] = null;
                                goto LOOP_FROM_TAIL;
                            }
                        }
                        catch (Exception e)
                        {
                            items[i] = null;
                            Debug.LogException(e);
                            goto LOOP_FROM_TAIL;
                        }
                    }

                    LOOP_FROM_TAIL:
                    while (i < j)
                    {
                        var fromTail = items[j];
                        if (fromTail != null)
                        {
                            try
                            {
                                if (fromTail.MoveNext())
                                {
                                    (items[i], items[j]) = (fromTail, null);
                                    j--;
                                    goto NEXT_I;
                                }
                                else
                                {
                                    items[j] = null;
                                    j--;
                                    goto NEXT_J;
                                }
                            }
                            catch (Exception e)
                            {
                                items[j] = null;
                                Debug.LogException(e);
                                j--;
                                goto NEXT_J;
                            }
                        }
                        else
                        {
                            j--;
                            goto NEXT_J;
                        }

                        NEXT_J:
                        continue;
                    }

                    NEXT_I:
                    continue;
                }
            }

            lock (lockRunOrQueue)
            {
                isRunning = false;

                while (waits.Count != 0)
                {
                    if (items.Length == tail) Array.Resize(ref items, checked(tail * 2));
                    items[++tail] = waits.Dequeue();
                }
            }
        }
    }
}