using System;
using System.Collections;
using System.Threading;
using NUnit.Framework;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Promises;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

namespace Ponito.Core.Tests
{
    public class AsyncsTests
    {
        [UnityTest]
        public IEnumerator TestDelay3000()
        {
            var before = Time.time;
            yield return PoTask.Delay(3000).RunAsCoroutine();
            Assert.IsTrue(Time.time - before > 3f);
        }

        [UnityTest]
        public IEnumerator TestYield()
        {
            var before = Time.frameCount;
            yield return PoTask.Yield().RunAsCoroutine();
            var frameCount = Time.frameCount;
            var frame      = frameCount - before;
            Assert.IsTrue(frame == 1, $"frameCount - before = {frameCount} - {before} = {frame}");
        }

        [UnityTest]
        public IEnumerator TestPoTaskDelay3000()
        {
            var before = Time.time;
            yield return PoTaskDelay3000().RunAsCoroutine();
            Assert.IsTrue(Time.time - before > 3f);
        }

        [UnityTest]
        public IEnumerator TestPoTaskYield50Frame()
        {
            var before = Time.frameCount;
            yield return PoTaskYield50Frame().RunAsCoroutine();
            var frameCount = Time.frameCount;
            var frame      = frameCount - before;
            Assert.IsTrue(frame == 50, $"frameCount - before = {frameCount} - {before} = {frame}");
        }

        [UnityTest]
        public IEnumerator TestPoTaskYield100Frame()
        {
            var before = Time.frameCount;
            yield return PoTaskYield100Frame().RunAsCoroutine();
            var frameCount = Time.frameCount;
            var frame      = frameCount - before;
            Assert.IsTrue(frame == 100, $"frameCount - before = {frameCount} - {before} = {frame}");
        }

        [UnityTest]
        public IEnumerator TestPoTaskInnerException()
        {
            Exception ex = null;
            yield return PoTaskInnerException()
               .Catch(exception => ex = exception)
               .RunAsCoroutine();
            Assert.NotNull(ex);
        }

        [UnityTest]
        public IEnumerator TestPoTaskYieldException()
        {
            Exception ex = null;
            yield return PoTaskYieldException()
               .Catch(exception => ex = exception)
               .RunAsCoroutine();
            Assert.NotNull(ex);
        }

        [UnityTest]
        public IEnumerator TestPoTask5()
        {
            using var i = new Promise<int>();
            yield return PoTask5().RunAsCoroutine(i);
            Assert.IsTrue(i == 5);
        }

        [UnityTest]
        public IEnumerator TestPoTask10()
        {
            using var i = new Promise<int>();
            yield return PoTask10().RunAsCoroutine(i);
            Assert.IsTrue(i == 10);
        }

        [UnityTest]
        public IEnumerator TestPoTaskCancel()
        {
            var       before = Time.frameCount;
            Exception ex     = null;

            yield return PoTaskCancel()
               .Catch(exception => ex = exception)
               .RunAsCoroutine();

            Assert.IsTrue(ex is OperationCanceledException);
            Debug.Log(ex.Message);

            var frameCount = Time.frameCount;
            var frame      = frameCount - before;
            Assert.IsTrue(frame == 5, $"frameCount - before = {frameCount} - {before} = {frame}");
        }
        
        private async PoTask PoTaskDelay3000()
        {
            await PoTask.Delay(3000);
        }

        private async PoTask PoTaskYield50Frame()
        {
            for (int i = 0; i < 50; i++) await PoTask.Yield();
        }

        private async PoTask PoTaskYield100Frame()
        {
            await PoTaskYield50Frame();
            await PoTaskYield50Frame();
        }

        private async PoTask PoTaskYieldException()
        {
            await PoTask.Yield();
            await PoTaskInnerException();
        }

        private async PoTask PoTaskInnerException()
        {
            throw new Exception();
        }

        private async PoTask<int> PoTask5()
        {
            await PoTask.Delay(1f);
            return 5;
        }

        private async PoTask<int> PoTask10()
        {
            return await PoTask5() + await PoTask5();
        }

        private async PoTask PoTaskCancel()
        {
            var cts = new CancellationTokenSource();
            for (int i = 0; i < 5; i++) await PoTask.Yield();
            cts.Cancel();
            for (int i = 0; i < 5; i++) await PoTask.Yield(cts.Token);
        }
    }
}