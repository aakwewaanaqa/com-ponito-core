using System.Collections;
using UnityEngine;

namespace Ponito.Core.Ease
{
    internal partial class Easer<T>
    {
        public void   Reset() => time = 0f;
        public object Current => lerper(start, end, easeFunction(time / duration));

        public bool MoveNext()
        {
            if (!isEnded)
            {
                setter((T)Current);
                time += Time.deltaTime;
                return true;
            }

            isEnded = true;
            setter?.Invoke(end); // Weird bug when animation disposed...
            Dispose();
            return false;
        }
    }
}