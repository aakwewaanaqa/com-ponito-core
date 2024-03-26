using System;
using Ponito.Core.Java;
using UnityEngine;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        public static void Toast(this string msg)
        {
            if (Application.platform is not RuntimePlatform.Android)
            {
                Debug.Log(msg);
                return;
            }

            try
            {
                var activity = new UnityActivity();
                var toast    = new AndroidJavaClass("android.widget.Toast");
                var makeText = toast.CallStatic<AndroidJavaObject>("makeText", (AndroidJavaObject)activity, msg, 0);
                makeText.Call("show");
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }
    }
}