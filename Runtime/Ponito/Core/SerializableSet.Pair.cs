using System;
using System.Collections.Generic;

namespace Ponito.Core
{
    public partial class SerializableSet<TKey, TValue>
    {
        [Serializable]
        public struct Pair : IComparable<Pair>, IEquatable<TKey>
        {
            public TKey   key;
            public TValue value;

            public static implicit operator KeyValuePair<TKey, TValue>(Pair pair)
            {
                return new KeyValuePair<TKey, TValue>(pair.key, pair.value);
            }

            public static explicit operator Pair(KeyValuePair<TKey, TValue> pair)
            {
                return new Pair
                {
                    key   = pair.Key,
                    value = pair.Value
                };
            }

            public bool Equals(TKey other)
            {
                return key.Equals(other);
            }

            public override int GetHashCode()
            {
                return key.GetHashCode();
            }

            public int CompareTo(Pair other)
            {
                return GetHashCode().CompareTo(other.GetHashCode());
            }
        }
    }
}