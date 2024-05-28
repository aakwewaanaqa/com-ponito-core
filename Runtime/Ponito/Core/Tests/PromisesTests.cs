using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
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

        [UnityTest]
        public IEnumerator TestXml()
        {
            const string URL         = "https://www.cwa.gov.tw/rss/forecast/36_01.xml";
            using var    reader      = XmlReader.Create(URL);
            var          stack       = new Stack<string>();
            var          description = new StringBuilder();
            while (reader.Read())
            {
                if (reader.NodeType is XmlNodeType.Element) stack.Push(reader.Name);
                if (reader.NodeType is XmlNodeType.EndElement) stack.Pop();

                if (stack.TryPeek(out var peek) && peek == "description") description.Append(reader.Value);
            }

            Debug.Log(description);
            yield break;
        }

        private string AggregateStack(Stack<string> stack)
        {
            return stack.Reverse().Aggregate((a, b) => $"{a}.{b}");
        }

        private static IEnumerator WaitFor5()
        {
            yield return new WaitForSeconds(5f);
        }
    }
}