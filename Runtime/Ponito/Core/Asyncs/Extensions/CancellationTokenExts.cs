using System;
using System.Threading;

namespace Ponito.Core.Asyncs.Extensions
{
    public static class CancellationTokenExts
    {
        public static CancellationTokenRegistration RegisterWithoutCaptureExecutionContext(this CancellationToken ct, Action<object> callback, object state)
        {
            // var restoreFlow = false;
            // if (!ExecutionContext.IsFlowSuppressed())
            // {
            //     ExecutionContext.SuppressFlow();
            //     restoreFlow = true;
            // }
            //
            // try
            // {
            //     return cancellationToken.Register(callback, state, false);
            // }
            // finally
            // {
            //     if (restoreFlow)
            //     {
            //         ExecutionContext.RestoreFlow();
            //     }
            // }
            return ct.Register(callback, state, false);
        }
    }
}