using System;
using System.Collections.Generic;
using Unity.Android.Types;

namespace Ponito.Core.Ease
{
    public class EasableManager : MonoSingleton<EasableManager>
    {
        private static List<Easable> easables { get; } = new();

        protected override bool IsInitialized       => true;
        protected override bool IsDontDestroyOnLoad => true;

        protected override void Initialize()
        {
        }

        public void Run(Easable easable)
        {
            easables.Add(easable);
        }

        private void Update()
        {
            for (int i = 0; i < easables.Count; i++)
            {
                AGAIN:
                
                if (i >= easables.Count) break;
                
                if (easables[i] != null)
                {
                    if (easables[i].MoveNext()) continue;
                    
                    easables.RemoveAt(i);
                    goto AGAIN;
                }

                easables.RemoveAt(i);
                goto AGAIN;
            }
        }
    }
}