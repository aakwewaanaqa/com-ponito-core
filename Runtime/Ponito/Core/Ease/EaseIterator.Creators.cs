using UnityEngine;

namespace Ponito.Core.Ease
{
    public partial class EaseEnumerator<T>
    {
        public static EaseEnumerator<float> Float(
            float start,
            float end,
            Setter<float> setter,
            float duration,
            EaseType easeType = EaseType.InSine)
            => new(start, end, setter, LerperOfFloat, duration);

        public static EaseEnumerator<Vector2> Vector2(
            Vector2 start,
            Vector2 end,
            Setter<Vector2> setter,
            float duration,
            EaseType easeType = EaseType.InSine) =>
            new(start, end, setter, LerperOfVector2, duration, easeType);

        public static EaseEnumerator<Vector3> Vector3(
            Vector3 start,
            Vector3 end,
            Setter<Vector3> setter,
            float duration,
            EaseType easeType = EaseType.InSine) =>
            new(start, end, setter, LerperOfVector3, duration, easeType);

        public static EaseEnumerator<Quaternion> Quaternion(
            Quaternion start,
            Quaternion end,
            Setter<Quaternion> setter,
            float duration,
            EaseType easeType = EaseType.InSine) =>
            new(start, end, setter, LerperOfQuaternion, duration, easeType);
    }
}