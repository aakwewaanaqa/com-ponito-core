using UnityEngine;

namespace Ponito.Core.Java
{
    public class JavaUri : AAndroidJava
    {
        public AndroidJavaObject JavaObject
        {
            get => javaObject;
            set => javaObject = value;
        }

        public JavaUri() : base("android.net.Uri")
        {
        }

        public JavaUri(JavaFile file) : this()
        {
            javaObject = javaClass.CallStatic<AndroidJavaObject>(nameof(fromFile), file.Object);
        }

        public JavaUri fromFile(AndroidJavaObject file)
        {
            javaObject = javaClass.CallStatic<AndroidJavaObject>(nameof(fromFile), file);
            return this;
        }
    }
}