using Ponito.Core.Extensions;
using UnityEngine;

namespace Ponito.Core.Math
{
    public struct Segment2D
    {
        public Vector2 a;
        public Vector2 b;

        public float XMax                   => Mathf.Max(a.x, b.x);
        public float XMin                   => Mathf.Min(a.x, b.x);
        public float YMax                   => Mathf.Max(a.y, b.y);
        public float YMin                   => Mathf.Min(a.y, b.y);
        public Rect  Rectangle              => Rect.MinMaxRect(XMin, YMin, XMax, YMax);
        public bool  PointInXRange(Vector2 p)    => p.x > XMin  && p.x < XMax;
        public bool  PointInYRange(Vector2 p)    => p.y > YMin  && p.y < YMax;
        public bool  PointInRect(Vector2 p) => PointInXRange(p) && PointInYRange(p);
    }
}