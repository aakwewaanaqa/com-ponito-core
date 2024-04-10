using System;
using Ponito.Core.Extensions;
using UnityEngine;

namespace Ponito.Core.Math
{
    [Serializable]
    public readonly struct Segment2D
    {
        public readonly Vector2 a;
        public readonly Vector2 b;

        public Segment2D(Vector2 a, Vector2 b)
        {
            (this.a, this.b) = (a, b);
        }

        public Segment2D(float aX, float aY, float bX, float bY)
        {
            (a, b) = (new Vector2(aX, aY), new Vector2(bX, bY));
        }

        public Vector2 Norm => (b - a).normalized;
        public float   XMax => Mathf.Max(a.x, b.x);
        public float   XMin => Mathf.Min(a.x, b.x);
        public float   YMax => Mathf.Max(a.y, b.y);
        public float   YMin => Mathf.Min(a.y, b.y);

        public bool SlopePositivity
        {
            get
            {
                var seg = b - a;
                return seg is { x: > 0, y: > 0 } or { x: < 0, y: < 0 };
            }
        }

        public bool IsHorizontal => Norm.y == 0f;
        public bool IsVertical   => Norm.x == 0f;

        public bool PointInXRange(Vector2 p)
        {
            return p.x >= XMin && p.x <= XMax;
        }

        public bool PointInYRange(Vector2 p)
        {
            return p.y >= YMin && p.y <= YMax;
        }

        public bool PointIsOnSegment(Vector2 p)
        {
            if ((a.x.Approx(p.x) && a.y.Approx(p.y)) ||
                (b.x.Approx(p.x) && b.y.Approx(p.y))) return true;

            if ((IsVertical || IsHorizontal) &&
                PointInXRange(p)             && PointInYRange(p)) return true;

            var (seg, v) = (Norm, (p - a).normalized);
            return (seg.x / v.x).Approx(seg.y / v.y);
        }

        public bool PointIsAbove(Vector2 p)
        {
            if (!PointInXRange(p)) return false;
            if (IsVertical) return p.y > YMax;
            if (PointIsOnSegment(p)) return false;

            var (seg, v) = (Norm, (p - a).normalized);
            return v.y > seg.y;
        }

        public bool PointIsBelow(Vector2 p)
        {
            if (!PointInXRange(p)) return false;
            if (IsVertical) return p.y < YMin;
            if (PointIsOnSegment(p)) return false;

            var (seg, v) = (Norm, (p - a).normalized);
            return v.y < seg.y;
        }

        public bool PointIsOnLeft(Vector2 p)
        {
            if (!PointInYRange(p)) return false;
            if (IsHorizontal) return p.x < XMin;
            if (PointIsOnSegment(p)) return false;

            var (seg, v) = (Norm, (p - a).normalized);
            return v.x < seg.x;
        }

        public bool PointIsOnRight(Vector2 p)
        {
            if (!PointInYRange(p)) return false;
            if (IsHorizontal) return p.x > XMax;
            if (PointIsOnSegment(p)) return false;

            var (seg, v) = (Norm, (p - a).normalized);
            return v.x > seg.x;
        }
    }
}