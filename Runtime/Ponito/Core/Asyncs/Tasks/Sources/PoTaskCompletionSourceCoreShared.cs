﻿using System;

namespace Ponito.Core.Asyncs.Tasks.Sources
{
    internal static class PoTaskCompletionSourceCoreShared
    {
        public static readonly Action<object> sentinel = Completion;

        private static void Completion(object _)
        {
            throw new InvalidOperationException();
        }
    }
}