using System.Collections;
using NUnit.Framework;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Tests
{
    public partial class AsyncsTestsMono
    {
        [SerializeField] [ForTest] private bool testYield5Frame;
        [SerializeField] [ForTest] private bool testYield5FrameCoroutine;
        [SerializeField] [ForTest] private bool testYield5FramePoTaskCoroutine;

        private async PoTask Yield5Frame()
        {
            var before = Time.frameCount;
            for (int i = 0; i < 5; i++) await PoTask.Yield();
            var frame = Time.frameCount - before;
            testYield5Frame = frame == 5;
            Assert.That(frame, Is.EqualTo(5));
        }

        private IEnumerator Yield5FrameCoroutine()
        {
            var before = Time.frameCount;
            for (int i = 0; i < 5; i++) yield return PoTask.Yield().WaitAsCoroutine();
            var frame = Time.frameCount - before;
            testYield5FrameCoroutine = frame == 5;
            Assert.That(frame, Is.EqualTo(5));
        }

        private async PoTask Yield5FramePoTaskCoroutine()
        {
            var before = Time.frameCount;
            for (int i = 0; i < 5; i++) await PoTask.Yield();
            var frame = Time.frameCount - before;
            testYield5FramePoTaskCoroutine = frame == 5;
            Assert.That(frame, Is.EqualTo(5));
        }
    }
}