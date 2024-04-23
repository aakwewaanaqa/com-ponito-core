using System.Collections;

namespace Ponito.Core.Asyncronized
{
    public static class PoTaskExts
    {
        public static PoTask AsPoTask(this IEnumerator ie)
        {
            return new PoTask(ie);
        }
    }
}