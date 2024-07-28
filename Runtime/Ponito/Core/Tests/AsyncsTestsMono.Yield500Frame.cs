using System.Collections;
using NUnit.Framework;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Tests
{
    public partial class AsyncsTestsMono
    {
        [SerializeField] [ForTest] private bool testYield500Frame;
        [SerializeField] [ForTest] private bool testYield500FrameCoroutine;
        [SerializeField] [ForTest] private bool testYield500FramePoTaskCoroutine;

        private async PoTask Yield500Frame()
        {
            var before = Time.frameCount;
            for (int i = 0; i < 500; i++) await PoTask.Yield();
            var frame = Time.frameCount - before;
            testYield500Frame = frame == 500;
            Assert.That(frame, Is.EqualTo(500));
        }

        private IEnumerator Yield500FrameCoroutine()
        {
            var before = Time.frameCount;
            for (int i = 0; i < 500; i++) yield return PoTask.Yield().WaitAsCoroutine();
            var frame = Time.frameCount - before;
            testYield500FrameCoroutine = frame == 500;
            Assert.That(frame, Is.EqualTo(500));
        }

        private async PoTask Yield500FramePoTaskCoroutine()
        {
            var before = Time.frameCount;
            for (int i = 0; i < 500; i++) await PoTask.Yield();
            var frame = Time.frameCount - before;
            testYield500FramePoTaskCoroutine = frame == 500;
            Assert.That(frame, Is.EqualTo(500));
        }
    }
}