using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace Ponito.Core.Pools
{
    public class UseListPool<T> : IDisposable, IEnumerable<T>
    {
        public readonly List<T> list = ListPool<T>.Get();

        public void Dispose()
        {
            ListPool<T>.Release(list);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)list).GetEnumerator();
        }

        public T this[int i] => list[i];
        public int  Count                          => list.Count;
        public bool Contains(T t)                  => list.Contains(t);
        public void AddRange(IEnumerable<T> range) => list.AddRange(range);
        public void Add(T t)                       => list.Add(t);
        public void Sort()                         => list.Sort();
        public int  BinarySearch(T t)              => list.BinarySearch(t);

        public UseListPool(params IEnumerable<T>[] ranges)
        {
            foreach (var range in ranges) list.AddRange(range);
        }
    }
}