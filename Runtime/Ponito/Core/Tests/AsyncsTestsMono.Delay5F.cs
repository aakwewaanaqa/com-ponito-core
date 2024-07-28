using System.Collections;
using NUnit.Framework;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Tests
{
    public partial class AsyncsTestsMono
    {
        [SerializeField] [ForTest] private bool testDelay5F;
        [SerializeField] [ForTest] private bool testDelay5FCoroutine;
        [SerializeField] [ForTest] private bool testDelay5FPoTaskCoroutine;
        
        private async PoTask Delay5F()
        {
            var before = Time.time;
            await 5f.Delay();
            var delta = Time.time - before;
            testDelay5F = delta >= 5f;
            Assert.That(delta, Is.GreaterThanOrEqualTo(5f));
        }

        private IEnumerator Delay5FCoroutine()
        {
            var before = Time.time;
            yield return 5f.Delay().WaitAsCoroutine();
            var delta = Time.time - before;
            testDelay5FCoroutine = delta >= 5f;
            Assert.That(delta, Is.GreaterThanOrEqualTo(5f));
        }

        private async PoTask Delay5FPoTaskCoroutine()
        {
            var before = Time.time;
            await 5f.Delay();
            var delta = Time.time - before;
            testDelay5FPoTaskCoroutine = delta >= 5f;
            Assert.That(delta, Is.GreaterThanOrEqualTo(5f));
        }
    }
}