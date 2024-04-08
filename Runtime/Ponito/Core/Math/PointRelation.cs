using System;

namespace Ponito.Core.Math
{
    [Flags]
    public enum PointRelation
    {
        None    = 0,
        Above   = 1 << 1,
        Below   = 1 << 2,
        Left    = 1 << 3,
        Right   = 1 << 4,
        Inline  = 1 << 5,
        OnPoint = 1 << 6,
    }
}