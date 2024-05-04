using System;
using System.Threading;
using Ponito.Core.Asyncs.Tasks.Sources;

namespace Ponito.Core.Asyncs.Tasks
{
    public readonly partial struct PoTask
    {
        public static readonly PoTask CompletedTask =
            new();

        private static readonly PoTask CanceledUniTask =
            new Func<PoTask>(
                () => new PoTask(
                    new CanceledResultSource(CancellationToken.None), 0))();

        public static PoTask FromCanceled(CancellationToken ct = default)
        {
            return ct == CancellationToken.None 
                       ? CanceledUniTask 
                       : new PoTask(new CanceledResultSource(ct), 0);
        }
        
        public static PoTask<T> FromCanceled<T>(CancellationToken ct = default)
        {
            return ct == CancellationToken.None 
                       ? CanceledUniTaskCache<T>.Task
                       : new PoTask<T>(new CanceledResultSource<T>(ct), 0);
        }

        public static PoTask FromException(Exception ex)
        {
            return ex is OperationCanceledException oce
                       ? FromCanceled(oce.CancellationToken)
                       : new PoTask(new ExceptionResultSource(ex), 0);
        }
        
        public static PoTask<T> FromException<T>(Exception ex)
        {
            return ex is OperationCanceledException oce
                       ? FromCanceled<T>(oce.CancellationToken)
                       : new PoTask<T>(new ExceptionResultSource<T>(ex), 0);
        }

        public static PoTask<T> FromResult<T>(T result)
        {
            return new PoTask<T>(result);
        }
        
        private static class CanceledUniTaskCache<T>
        {
            public static readonly PoTask<T> Task;

            static CanceledUniTaskCache()
            {
                Task = new PoTask<T>(new CanceledResultSource<T>(CancellationToken.None), 0);
            }
        }
    }
}