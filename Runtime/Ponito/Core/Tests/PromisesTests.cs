using System;
using System.Collections;
using NUnit.Framework;
using Ponito.Core.Asyncs.Promises;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

namespace Ponito.Core.Tests
{
    public class PromisesTests
    {
        [UnityTest]
        public IEnumerator TestWaitFor5()
        {
            var       t = Time.time;
            using var p = new Promise().Run(WaitFor5());
            while (p.IsDoing) yield return new WaitForEndOfFrame();
            Assert.IsTrue(Time.time - t >= 5f);
        }

        [UnityTest]
        public IEnumerator TestException()
        {
            var i = 0;
            yield return new Promise()
               .Run(async () =>
                {
                    await PoTask.Delay(1f);
                    if (++i < 5) throw new Exception($"i = {i}");
                })
               .TryAgain(5, Debug.Log)
               .AsCoroutine();
            Assert.IsTrue(i == 5);
        }

        private IEnumerator WaitFor5()
        {
            yield return new WaitForSeconds(5f);
        }
    }
}