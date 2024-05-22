using System.Collections;
using NUnit.Framework;
using Ponito.Core.Asyncs;
using Ponito.Core.Asyncs.Promises;
using Ponito.Core.Asyncs.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

namespace Ponito.Core.Tests
{
    public class PromisesTests
    {
        [UnityTest]
        public IEnumerator TestWaitFor5()
        {
            var t = Time.time;
            using var p = new Promise().Run(WaitFor5());
            while (p.IsDoing) yield return new WaitForEndOfFrame();
            Assert.IsTrue(Time.time - t >= 5f);
        }
        
        private IEnumerator WaitFor5()
        {
            yield return new WaitForSeconds(5f);
        }
    }
}