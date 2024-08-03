using System;
using System.Collections;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Tests
{
    public partial class AsyncsTestsMono
    {
        [SerializeField] private bool testExcept;
        [SerializeField] private bool testExceptPoTaskCoroutine;
        [SerializeField] private bool testInnerExcept;
        [SerializeField] private bool testInnerExceptPoTaskCoroutine;
        
        private async PoTask Except()
        {
            await Controls.Yield();
            throw new Exception();
        }

        private async PoTask InnerExcept()
        {
            await Controls.Yield();
            await Except();
        }
        
        private async PoTask ExceptPoTaskCoroutine()
        {
            await Controls.Yield();
            throw new Exception();
        }
        
        private async PoTask InnerExceptPoTaskCoroutine()
        {
            await Controls.Yield();
            await InnerExcept();
        }
    }
}