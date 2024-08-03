#if UNITY_EDITOR
using System;
using System.Collections;
using NUnit.Framework;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Extensions;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

namespace Ponito.Core.Tests
{
    public class AsyncsTests
    {
        [UnityTest]
        public IEnumerator TestMono()
        {
#if UNITY_EDITOR
            Selection.activeGameObject = new GameObject("AsyncsTestsMono").EnsureComponent(out AsyncsTestsMono mono);
#endif
            yield return mono.IsAllPassed();
            Assert.IsTrue(mono.isFinished);
            yield return new WaitForSeconds(3f);
        }

        [UnityTest]
        public IEnumerator TestDelay3000()
        {
            var before = Time.time;
            yield return PoTask.Delay(3000).WaitAsCoroutine();
            Assert.IsTrue(Time.time - before > 3f);
        }

        [UnityTest]
        public IEnumerator TestYield()
        {
            var before = Time.frameCount;
            yield return PoTask.Yield().WaitAsCoroutine();
            Assert.That(Time.frameCount - before, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator TestPoTaskInnerException()
        {
            Exception ex = null;
            yield return PoTaskInnerException()
               .Try(exception => ex = exception)
               .WaitAsCoroutine();
            Assert.NotNull(ex);
        }

        [UnityTest]
        public IEnumerator TestPoTaskYieldException()
        {
            Exception ex = null;
            yield return PoTaskYieldException()
               .Try(exception => ex = exception)
               .WaitAsCoroutine();
            Assert.NotNull(ex);
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
    }
}
#endif