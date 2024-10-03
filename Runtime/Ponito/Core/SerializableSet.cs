using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ponito.Core
{
    [Serializable]
    public class SerializableSet<K, V> : IDictionary<K, V>
    {
        [SerializeField] private List<K> keys;
        [SerializeField] private List<V> values;

        public SerializableSet(int capcity = 0)
        {
            keys   = new List<K>(capcity);
            values = new List<V>(capcity);
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            for (var i = 0; i < keys.Count; i++)
            {
                var k = keys[i];
                var v = values[i];
                yield return new KeyValuePair<K, V>(k, v);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<K, V> item)
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

        public bool Contains(KeyValuePair<K, V> item)
        {
            var hasKey   = keys.Contains(item.Key);
            var hasValue = values.Contains(item.Value);
            return hasKey && hasValue;
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            for (var i = 0; i < keys.Count; i++) array[arrayIndex + i] = new KeyValuePair<K, V>(keys[i], values[i]);
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public int  Count      => keys.Count;
        public bool IsReadOnly => false;

        public void Add(K key, V value)
        {
            if (keys.Contains(key)) return;

            keys.Add(key);
            values.Add(value);
        }

        public bool ContainsKey(K key)
        {
            return keys.Contains(key);
        }

        public bool Remove(K key)
        {
            var index = keys.IndexOf(key);
            if (index == -1) return false;

            keys.RemoveAt(index);
            values.RemoveAt(index);
            return true;
        }

        public bool TryGetValue(K key, out V value)
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

        public V this[K key]
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

        public ICollection<K> Keys   => keys;
        public ICollection<V> Values => values;
    }
}