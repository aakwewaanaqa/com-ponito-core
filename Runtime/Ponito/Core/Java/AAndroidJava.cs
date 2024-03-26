using UnityEngine;

namespace Ponito.Core.Java
{
    public abstract class AAndroidJava
    {
        protected readonly AndroidJavaClass  javaClass;
        protected          AndroidJavaObject javaObject;

        public AndroidJavaObject Object => javaObject;
        
        protected AAndroidJava(string typeName)
        {
            javaClass  = new AndroidJavaClass(typeName);
        }

        public static explicit operator AndroidJavaObject(AAndroidJava a)
        {
            return a.javaObject;
        }

        public override string ToString()
        {
            return javaObject.Call<string>("toString");
        }
    }
}