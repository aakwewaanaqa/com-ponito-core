using UnityEngine;

namespace Ponito.Core.Java
{
    /// <remarks>
    ///     https://developer.android.com/reference/android/content/Intent
    /// </remarks>
    public class Intent : AAndroidJava
    {
        public Intent() : base("android.content.Intent")
        {
            javaObject = new AndroidJavaObject("android.content.Intent");
        }

        public string ACTION_VIEW => javaClass.GetStatic<string>(nameof(ACTION_VIEW));
        public string ACTION_INSTALL_PACKAGE => javaClass.GetStatic<string>(nameof(ACTION_INSTALL_PACKAGE));
        public int FLAG_ACTIVITY_NEW_TASK => javaClass.GetStatic<int>(nameof(FLAG_ACTIVITY_NEW_TASK));
        public int FLAG_GRANT_READ_URI_PERMISSION => javaClass.GetStatic<int>(nameof(FLAG_GRANT_READ_URI_PERMISSION));

        /// <remarks>
        ///     https://developer.android.com/reference/android/content/Intent#setAction(java.lang.String)
        /// </remarks>
        public Intent setAction(string action)
        {
            javaObject = javaObject.Call<AndroidJavaObject>(nameof(setAction), action);
            return this;
        }

        /// <remarks>
        ///     https://developer.android.com/reference/android/content/Intent#setType(java.lang.String)
        /// </remarks>
        public Intent setType(string type)
        {
            javaObject = javaObject.Call<AndroidJavaObject>(nameof(setType), type);
            return this;
        }

        /// <summary>
        ///     設定<see cref="Intent" />的資料路徑
        /// </summary>
        /// <param name="uri">資料路徑</param>
        /// <returns>自己，可以用來連鎖呼叫</returns>
        public Intent setData(JavaUri uri)
        {
            javaObject = javaObject.Call<AndroidJavaObject>(nameof(setData), (AndroidJavaObject)uri);
            return this;
        }

        /// <summary>
        ///     設定<see cref="Intent" />的額外標籤
        /// </summary>
        /// <param name="flags">額外標籤</param>
        /// <returns>自己，可以用來連鎖呼叫</returns>
        public Intent addFlags(int flags)
        {
            javaObject = javaObject.Call<AndroidJavaObject>(nameof(addFlags), flags);
            return this;
        }

        /// <summary>
        ///     設定<see cref="Intent" />要呼叫的標的命名空間與類別名稱
        /// </summary>
        /// <param name="packageName">命名空間，在Java裡面稱呼為package</param>
        /// <param name="className">類別名稱</param>
        /// <returns>自己，可以用來連鎖呼叫</returns>
        public Intent setClassName(string packageName, string className)
        {
            javaObject = javaObject.Call<AndroidJavaObject>(nameof(setClassName), packageName, className);
            return this;
        }

        public static Intent CreateInstallAPKIntent(string apkPath)
        {
            var javaFile = new JavaFile(apkPath);
            var javaUri  = new JavaUri(javaFile);

            var intent = new Intent();
            intent.setAction(intent.ACTION_INSTALL_PACKAGE)
               .setData(javaUri)
               .setType("application/vnd.android.package-archive")
               .addFlags(intent.FLAG_ACTIVITY_NEW_TASK)
               .addFlags(intent.FLAG_GRANT_READ_URI_PERMISSION);
            // .setClassName("com.android.packageinstaller", "com.android.packageinstaller.PackageInstallerActivity");

            return intent;
        }
    }
}