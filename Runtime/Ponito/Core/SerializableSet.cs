using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ponito.Core
{
    [Serializable]
    public class SerializableSet<TKey, TValue> : IDictionary<TKey, TValue>
    {
        [SerializeField] private List<TKey>   keys;
        [SerializeField] private List<TValue> values;

        public SerializableSet(int capcity = 0)
        {
            keys   = new List<TKey>(capcity);
            values = new List<TValue>(capcity);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (var i = 0; i < keys.Count; i++)
            {
                var k = keys[i];
                var v = values[i];
                yield return new KeyValuePair<TKey, TValue>(k, v);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (keys.Contains(item.Key)) return;

            keys.Add(item.Key);
            values.Add(item.Value);
        }

        public void Clear()
        {
            keys.Clear();
            values.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var hasKey   = keys.Contains(item.Key);
            var hasValue = values.Contains(item.Value);
            return hasKey && hasValue;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            for (var i = 0; i < keys.Count; i++)
                array[arrayIndex + i] = new KeyValuePair<TKey, TValue>(keys[i], values[i]);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public int  Count      => keys.Count;
        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            if (keys.Contains(key)) return;

            keys.Add(key);
            values.Add(value);
        }

        public bool ContainsKey(TKey key)
        {
            return keys.Contains(key);
        }

        public bool Remove(TKey key)
        {
            var index = keys.IndexOf(key);
            if (index == -1) return false;

            keys.RemoveAt(index);
            values.RemoveAt(index);
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var index = keys.IndexOf(key);
            if (index == -1)
            {
                value = default;
                return false;
            }

            value = values[index];
            return true;
        }

        public TValue this[TKey key]
        {
            get => values[keys.IndexOf(key)];
            set
            {
                if (!keys.Contains(key))
                {
                    keys.Add(key);
                    values.Add(value);
                    return;
                }

                var index = keys.IndexOf(key);
                values[index] = value;
            }
        }

        public ICollection<TKey>   Keys   => keys;
        public ICollection<TValue> Values => values;
    }
}