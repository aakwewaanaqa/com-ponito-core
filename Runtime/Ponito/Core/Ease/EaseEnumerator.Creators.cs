using System.Threading;
using UnityEngine;

namespace Ponito.Core.Ease
{
    internal partial class Easer<T>
    {
        public static Easer<float> Float(
            float start,
            float end,
            Setter<float> setter,
            float duration,
            EaseType easeType = EaseType.InSine,
            CancellationToken ct = default)
        {
            return new Easer<float>(start, end, setter, LerperOfFloat, duration, easeType, ct);
        }

        public static Easer<Vector2> Vector2(
            Vector2 start,
            Vector2 end,
            Setter<Vector2> setter,
            float duration,
            EaseType easeType = EaseType.InSine,
            CancellationToken ct = default)
        {
            return new Easer<Vector2>(start, end, setter, LerperOfVector2, duration, easeType, ct);
        }

        public static Easer<Vector3> Vector3(
            Vector3 start,
            Vector3 end,
            Setter<Vector3> setter,
            float duration,
            EaseType easeType = EaseType.InSine,
            CancellationToken ct = default)
        {
            return new Easer<Vector3>(start, end, setter, LerperOfVector3, duration, easeType, ct);
        }

        public static Easer<Quaternion> Quaternion(
            Quaternion start,
            Quaternion end,
            Setter<Quaternion> setter,
            float duration,
            EaseType easeType = EaseType.InSine,
            CancellationToken ct = default)
        {
            return new Easer<Quaternion>(start, end, setter, LerperOfQuaternion, duration, easeType, ct);
        }
    }
}