using System;
using System.Collections;
using NUnit.Framework;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Tests
{
    public partial class AsyncsTestsMono
    {
        [SerializeField] [ForTest] private bool testYield;
        [SerializeField] [ForTest] private bool testYieldCoroutine;
        [SerializeField] [ForTest] private bool testYieldPoTaskCoroutine;

        private async PoTask Yield()
        {
            var before = Time.frameCount;
            await PoTask.Yield();
            var frame = Time.frameCount - before;
            testYield = frame == 1;
            Assert.That(frame, Is.EqualTo(1));
        }

        private IEnumerator YieldCoroutine()
        {
            var before = Time.frameCount;
            yield return PoTask.Yield().WaitAsCoroutine();
            var frame = Time.frameCount - before;
            testYieldCoroutine = frame == 1;
            Assert.That(frame, Is.EqualTo(1));
        }

        private async PoTask YieldPoTaskCoroutine()
        {
            var before = Time.frameCount;
            await PoTask.Yield();
            var frame = Time.frameCount - before;
            testYieldPoTaskCoroutine = frame == 1;
            Assert.That(frame, Is.EqualTo(1));
        }
    }
}