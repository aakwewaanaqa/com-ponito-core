using System;
using UnityEngine;

namespace Ponito.DIAttributes
{
    public class DIObjectPicker : PropertyAttribute
    {
        public Type type { get; set; }

        public DIObjectPicker(Type type)
        {
            this.type = type;
        }
    }
}