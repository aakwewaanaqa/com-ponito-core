using System;
using System.Collections.Generic;
using System.Linq;

namespace Ponito.Core.Extensions
{
    public static class IEnumerableExts
    {
        public static IEnumerable<TObject> ForEach<TObject>(this IEnumerable<TObject> self, Action<TObject, int> indexer)
        {
            for (int i = 0; i < self.Count(); i++) indexer(self.ElementAt(i), i);
            return self;
        }
        
        public static IEnumerable<TObject> ForEach<TObject>(this IEnumerable<TObject> self, Action<TObject> indexer)
        {
            for (int i = 0; i < self.Count(); i++) indexer(self.ElementAt(i));
            return self;
        }
    }
}