using System.Collections;
using NUnit.Framework;
using Ponito.Core.Asyncs;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Tests
{
    public partial class AsyncsTestsMono
    {
        [SerializeField] [ForTest] private bool testMath;
        [SerializeField] [ForTest] private bool testMathCoroutine;
        [SerializeField] [ForTest] private bool testMathPoTaskCoroutine;

        private static async PoTask<int> Five()
        {
            await PoTask.Yield();
            return 5;
        }

        private static async PoTask<int> Three()
        {
            await PoTask.Yield();
            return 3;
        }

        private async PoTask Math()
        {
            var result = await Five() + await Three();
            Assert.That(result, Is.EqualTo(5 + 3));
            testMath = result == 5 + 3;
        }

        private IEnumerator MathCoroutine()
        {
            var result1 = new CoroutineResult<int>();
            var result2 = new CoroutineResult<int>();
            yield return StartCoroutine(Five().WaitAsCoroutine(result1));
            yield return StartCoroutine(Three().WaitAsCoroutine(result2));
            var result = result1.value + result2.value;
            Assert.That(result, Is.EqualTo(5 + 3));
            testMathCoroutine = result == 5 + 3;
        }

        private async PoTask MathPoTaskCoroutine()
        {
            var result = await Three() + await Five();
            Assert.That(result, Is.EqualTo(5 + 3));
            testMathPoTaskCoroutine = result == 5 + 3;
        }
    }
}