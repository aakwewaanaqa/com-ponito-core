using System.Collections;
using Ponito.Core.Asyncs;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Tests
{
    public partial class AsyncsTestsMono
    {
        [SerializeField] [ForTest] private bool testResult;
        [SerializeField] [ForTest] private bool testResultCoroutine;
        [SerializeField] [ForTest] private bool testResultPoTaskCoroutine;

        private async PoTask<bool> Result()
        {
            await Controls.Delay(1f);
            return true;
        }

        private IEnumerator ResultCoroutine(CoroutineResult<bool> result)
        {
            yield return Controls.Delay(1f).WaitAsCoroutine();
            if (result != null) result.value = true;
        }

        private async PoTask<bool> ResultPoTaskCoroutine()
        {
            await Controls.Delay(1f);
            return true;
        }
    }
}