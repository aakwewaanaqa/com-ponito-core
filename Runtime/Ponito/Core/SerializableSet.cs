using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ponito.Core
{
    [Serializable]
    public partial class SerializableSet<TKey, TValue> : IDictionary<TKey, TValue>
    {
        [SerializeField] private List<Pair> pairs;

        public SerializableSet(int capcity = 0)
        {
            pairs = new List<Pair>(capcity);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var pair in pairs) yield return pair;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (ContainsKey(item.Key)) return;
            pairs.Add((Pair)item);
        }

        public void Clear()
        {
            pairs.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var target = (Pair)item;
            return pairs.Contains(target);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            for (var i = 0; i < Count; i++) array[arrayIndex + i] = pairs[i];
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            var target = (Pair)item;
            return pairs.Remove(target);
        }

        public int  Count      => pairs.Count;
        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key)) return;
            pairs.Add(new Pair { key = key, value = value });
        }

        public bool ContainsKey(TKey key)
        {
            return pairs.Exists(p => p.key.Equals(key));
        }

        public bool Remove(TKey key)
        {
            if (!ContainsKey(key)) return false;
            var index = pairs.FindIndex(p => p.key.Equals(key));
            pairs.RemoveAt(index);
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default;
            if (!ContainsKey(key)) return false;
            value = pairs.Find(p => p.key.Equals(key)).value;
            return true;
        }

        public TValue this[TKey key]
        {
            get => pairs.Find(p => p.key.Equals(key)).value;
            set
            {
                if (!ContainsKey(key))
                {
                    Add(new Pair { key = key, value = value });
                    return;
                }

                var index = pairs.FindIndex(p => p.key.Equals(key));
                pairs[index] = new Pair { key = key, value = value };
            }
        }

        public ICollection<TKey>   Keys   => pairs.ConvertAll(pair => pair.key);
        public ICollection<TValue> Values => pairs.ConvertAll(pair => pair.value);
    }
}