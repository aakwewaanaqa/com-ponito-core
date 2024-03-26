using UnityEngine;

namespace Ponito.Core.Java
{
    public class JavaFile : AAndroidJava
    {
        public JavaFile(params object[] paths) : base("java.io.File")
        {
            javaObject = new AndroidJavaObject("java.io.File", paths);
        }

        public bool exists()
        {
            return javaObject.Call<bool>(nameof(exists));
        }
        
        public void createNewFile()
        {
            javaObject.Call<bool>(nameof(createNewFile));
        }

        public string getAbsolutePath()
        {
            return javaObject.Call<string>(nameof(getAbsolutePath));
        }
    }
}