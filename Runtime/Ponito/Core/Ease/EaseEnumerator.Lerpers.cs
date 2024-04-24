using UnityEngine;

namespace Ponito.Core.Ease
{
    internal partial class EaseEnumerator<T>
    {
        /// <inheritdoc cref="Mathf.Lerp" />
        public static Lerper<float> LerperOfFloat => Mathf.Lerp;

        /// <inheritdoc cref="Vector2.Lerp" />
        public static Lerper<Vector2> LerperOfVector2 => UnityEngine.Vector2.Lerp;

        /// <inheritdoc cref="Vector3.Lerp" />
        public static Lerper<Vector3> LerperOfVector3 => UnityEngine.Vector3.Lerp;

        /// <inheritdoc cref="Quaternion.Lerp" />
        public static Lerper<Quaternion> LerperOfQuaternion => UnityEngine.Quaternion.Lerp;
    }
}