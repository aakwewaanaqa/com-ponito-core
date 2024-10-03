using System.Collections;
using System.Collections.Generic;
using Ponito.Core.Samples.UI;

namespace Ponito.Core.Samples.Managers
{
    public partial class PoStateManager
    {
        private readonly struct StateBind : IEnumerable<StateWatcher>
        {
            public readonly Statable           state;
            public readonly ISet<StateWatcher> watchers;

            public StateBind(Statable state)
            {
                this.state = state;
                watchers   = new HashSet<StateWatcher>();
            }

            public IEnumerator<StateWatcher> GetEnumerator()
            {
                foreach (var watcher in watchers) yield return watcher;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public bool TryWatch(StateWatcher watcher)
            {
                return watchers.Add(watcher);
            }

            public bool TryUnwatch(StateWatcher watcher)
            {
                return watchers.Remove(watcher);
            }
        }
    }
}