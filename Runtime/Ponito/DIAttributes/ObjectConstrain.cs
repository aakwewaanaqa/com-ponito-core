using System;
using UnityEngine;

namespace Ponito.DIAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ObjectConstrain : PropertyAttribute
    {
        public ObjectConstrain(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}