using System;
using System.Collections;
using System.Collections.Generic;

namespace Ponito.Core.Asyncs
{
    public class EnumeratorRunner : MonoSingleton<EnumeratorRunner>
    {
        protected override bool IsInitialized       => true;
        protected override bool IsDontDestroyOnLoad => true;

        protected override void Initialize()
        {
        }

        private List<IEnumerator> list { get; } = new();

        public void Queue(IEnumerator ie)
        {
            list.Add(ie);
        }

        private void Update()
        {
            for (int i = 0; i < list.Count; i++)
            {
                AGAIN:

                if (i >= list.Count) break;
                
                var head = list[i];
                if (head != null)
                {
                    if (head.MoveNext()) continue;
                }

                list.RemoveAt(i);
                goto AGAIN;
            }
        }
    }
}