using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Ponito.Core.Asyncs;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Tests
{
    [AddComponentMenu("Ponito/Core/Tests/Asyncs Tests Mono")]
    public partial class AsyncsTestsMono : MonoBehaviour
    {
        private async PoTask Start()
        {
            await PoTask.Delay(1f); // 不要在開始時執行測試，會有開始幀的問題

            await Yield();
            await Yield5Frame();
            await Yield500Frame();
            await Delay5F();
            testResult = await Result();

            StartCoroutine(StartAsCoroutine());
        }

        private IEnumerator StartAsCoroutine()
        {
            yield return StartCoroutine(YieldCoroutine());
            yield return StartCoroutine(YieldPoTaskCoroutine().WaitAsCoroutine());

            yield return StartCoroutine(Yield5FrameCoroutine());
            yield return StartCoroutine(Yield5FramePoTaskCoroutine().WaitAsCoroutine());
            
            yield return StartCoroutine(Yield500FrameCoroutine());
            yield return StartCoroutine(Yield500FramePoTaskCoroutine().WaitAsCoroutine());

            yield return StartCoroutine(Delay5FCoroutine());
            yield return StartCoroutine(Delay5FPoTaskCoroutine().WaitAsCoroutine());

            {
                var result1 = new CoroutineResult<bool>();
                var result2 = new CoroutineResult<bool>();
                yield return StartCoroutine(ResultCoroutine(result1));
                yield return StartCoroutine(ResultPoTaskCoroutine().WaitAsCoroutine(result2));
                testResultCoroutine       = result1.value;
                testResultPoTaskCoroutine = result2.value;
            }

            isFinished = true;
        }
    }
}