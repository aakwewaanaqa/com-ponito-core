using System;
using System.Collections.Generic;
using Ponito.Core.Extensions;
using Ponito.Core.Samples.UI;
using UnityEngine;

namespace Ponito.Core.Samples.Managers
{
    public partial class PoStateManager : MonoBehaviour
    {
        private readonly Dictionary<IntPtr, StateBind> binds = new();

        public bool Add(Statable statable)
        {
            return binds.TryAdd(statable.GetPtr(), new StateBind(statable));
        }

        public bool Remove(IntPtr ptr)
        {
            return binds.Remove(ptr);
        }

        public bool TryMutate(IntPtr ptr, object args)
        {
            var contains = binds.TryGetValue(ptr, out var bind);
            if (!contains) return false;
            var success = bind.state.TryMutate(args);
            if (success) bind.ForEach(w => w.NotifyChange(bind.state, args));
            return success;
        }

        public bool TryWatch(IntPtr ptr, StateWatcher watcher)
        {
            var contains = binds.TryGetValue(ptr, out var bind);
            return contains && bind.TryWatch(watcher);
        }

        public bool TryUnwatch(IntPtr ptr, StateWatcher watcher)
        {
            var contains = binds.TryGetValue(ptr, out var bind);
            return contains && bind.TryUnwatch(watcher);
        }
    }
}