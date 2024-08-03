using System.Collections;
using System.Threading;
using NUnit.Framework;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Tests
{
    public partial class AsyncsTestsMono
    {
        [SerializeField] [ForTest] private bool testCancelled;
        [SerializeField] [ForTest] private bool testCancelledPoTaskCoroutine;

        private async PoTask Cancelled()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();
            var before = Time.frameCount;
            for (int i = 0; i < 200; i++) await Controls.Yield(cts.Token);
            testCancelled = Time.frameCount == before;
            Assert.That(Time.frameCount, Is.EqualTo(before));
        }
        
        private async PoTask CancelledPoTaskCoroutine()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();
            var before = Time.frameCount;
            for (int i = 0; i < 200; i++) await Controls.Yield(cts.Token); // Coroutine can't be cancelled
            testCancelledPoTaskCoroutine = Time.frameCount == before;
            Assert.That(Time.frameCount, Is.EqualTo(before));
        }
    }
}