using System;

namespace Ponito.Core.DebugHelper
{
    public class DebugHelperScope : IDisposable
    {
        public DebugHelperScope(bool block = true)
        {
            IsBlocker = IsBlock = block;
        }

        public static bool IsBlock   { get; private set; }
        public        bool IsBlocker { get; }

        public void Dispose()
        {
            if (IsBlocker) IsBlock = false;
        }
    }
}