using System.Collections;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace Ponito.Core.Tests
{
    public partial class AsyncsTestsMono
    {
        [SerializeField] public bool isFinished;

        public IEnumerator IsAllPassed()
        {
            yield return new WaitUntil(() => isFinished);
            const BindingFlags FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;
            var result = typeof(AsyncsTestsMono)
               .GetFields(FLAGS)
               .Where(f => f.GetCustomAttribute<ForTest>() != null)
               .All(f => (bool)f.GetValue(this));
            Assert.IsTrue(result);
        }
    }
}