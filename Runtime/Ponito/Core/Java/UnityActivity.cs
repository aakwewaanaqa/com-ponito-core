using UnityEngine;

namespace Ponito.Core.Java
{
    public class UnityActivity : AAndroidJava
    {
        public UnityActivity() : base("com.unity3d.player.UnityPlayer")
        {
            javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
        }

        public void startActivity(Intent intent)
        {
            javaObject.Call(nameof(startActivity), (AndroidJavaObject)intent);
        }

        public AndroidJavaObject getApplicationContext()
        {
            return javaObject.Call<AndroidJavaObject>(nameof(getApplicationContext));
        }

        public string getPackageName()
        {
            var context = getApplicationContext();
            return context.Call<string>(nameof(getPackageName));
        }
    }
}