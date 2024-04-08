using UnityEngine;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        public static bool Approx(this Vector2 a, Vector2 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
        }
    }
}