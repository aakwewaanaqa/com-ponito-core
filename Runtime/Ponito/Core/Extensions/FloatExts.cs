using UnityEngine;

namespace Ponito.Core.Extensions
{
    public static class FloatExts
    {
        public static bool Approx(this float a, float b, float epsilon = 1E-8F)
        {
            var abs = Mathf.Abs(b - a);
            return abs < epsilon;
        }
    }
}