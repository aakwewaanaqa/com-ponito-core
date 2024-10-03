using System;

namespace Ponito.Core.Samples.UI
{
    public interface Statable
    {
        public bool TryMutate(object state);

        public IntPtr GetPtr()
        {
            return GetType().TypeHandle.Value;
        }
    }
}