using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    internal class ReflectAwait : MovableBase
    {
        private const BindingFlags FLAGS = BindingFlags.Public    |
                                           BindingFlags.NonPublic |
                                           BindingFlags.Instance;

        public ReflectAwait(INotifyCompletion awaiter)
        {
            var await    = "await".Colorize(DebugColors.KEYWORD_COLOR);
            var typeName = awaiter.GetType().Name.Colorize(DebugColors.CLASS_COLOR);
            $"{await} {typeName} by reflection".Warn();

            this.awaiter = awaiter;

            var type     = awaiter.GetType();
            var property = type.GetProperty("IsCompleted", FLAGS);
            var method   = property?.GetMethod;
            getIsCompleted = () => (bool)(method?.Invoke(awaiter, Array.Empty<object>()) ?? true);
        }

        private INotifyCompletion awaiter        { get; set; }
        private Func<bool>        getIsCompleted { get; set; }

        public override bool MoveNext()
        {
            if (Ct.IsCancellationRequested) return false;
            if (IsCompleted) return false;
            if (!getIsCompleted()) return true;
            return ContinueMoveNext();
        }

        public override void Dispose()
        {
            base.Dispose();
            getIsCompleted = null;
        }
    }
}