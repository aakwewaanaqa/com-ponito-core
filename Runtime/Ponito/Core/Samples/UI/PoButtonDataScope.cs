using System;
using Ponito.Core.Extensions;
using UnityEngine.EventSystems;

namespace Ponito.Core.Samples.UI
{
    public class PoButtonDataScope : IDisposable
    {
        public static IPointerDownHandler Pressing   { get; private set; }
        public static object              Holder     { get; private set; }
        public static bool                IsPressing => Pressing.IsObject();

        public PoButtonDataScope(object holder)
        {
            if (IsPressing) throw new InvalidOperationException($"{Holder} created before!");

            Holder = holder;
            if (holder is IPointerDownHandler pressing) Pressing = pressing;
        }

        public void Dispose()
        {
            Holder   = null;
            Pressing = null;
        }
    }
}