using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncs
{
    public partial class MovableRunner
    {
        public class RunnerItem : IDisposable
        {
            private object            state    { get; }
            private PoTask            task     { get; }
            private INotifyCompletion source   { get; }
            private Func<bool>        moveNext { get; }

            /// <summary>
            ///     取得 <see cref="RunnerItem"/> 的名字
            /// </summary>
            public string Name
            {
                get
                {
                    return source.GetType().Name;
                }
            }
            
            /// <summary>
            ///     取得 <see cref="RunnerItem"/> 的狀態
            /// </summary>
            public float Progress
            {
                get
                {
                    if (source is ProgressMovable p) return p.Progress;
                    return 0;
                }
            }

            public Exception Ex
            {
                get => task?.Ex;
                set
                {
                    if (task != null) task.Ex = value;
                }
            }

            public RunnerItem(PoTask task, INotifyCompletion source, object state)
            {
                this.task   = task;
                this.source = source;
                this.state  = state;

                task.SetSource(source);

                if (source is Movable movable)
                {
                    moveNext = () => movable.MoveNext();
                    return;
                }

                ("使用反射等待 " + source.GetType().Name).Warn();
                var property = source.GetType().GetProperty("IsCompleted");
                if (property != null)
                {
                    moveNext = () => !(bool)property.GetValue(source);
                    return;
                }

                moveNext = () => true;
            }

            public bool TryMoveNext()
            {
                return moveNext();
            }

            public void Dispose()
            {
                if (source is IDisposable disposable) disposable.Dispose();
                task?.TryClearSource(source);
            }
        }
    }
}