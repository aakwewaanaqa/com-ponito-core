using UnityEngine;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        public static bool Approx(this float a, float b)
        {
            return Mathf.Approximately(a, b);
        }
    }
}