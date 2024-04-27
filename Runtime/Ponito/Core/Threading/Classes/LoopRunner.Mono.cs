using System;
using System.Collections.Generic;
using Ponito.Core.Threading.Enums;
using Ponito.Core.Threading.Interfaces;
using UnityEngine;

namespace Ponito.Core.Threading.Classes
{
    public partial class LoopRunner : MonoSingleton<LoopRunner>
    {
        protected override bool IsDontDestroyOnLoad => true;

        protected override void Initialize()
        {
            name = $"{timing} ({typeof(LoopRunner)})";
        }

        private void Update()
        {
            if (timing is not Timing.Update) return;
            Run();
        }
    }
}