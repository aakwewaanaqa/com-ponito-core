using UnityEngine;
using UnityEngine.Networking;

namespace Ponito.Core.Asyncs.Promises
{
    /// <summary>
    ///     一個可以完整回報 <see cref="UnityWebRequest"/> 的類別
    /// </summary>
    public struct UnityWebResponse
    {
        /// <summary>
        ///     <see cref="UnityWebRequest"/> 的結果
        /// </summary>
        public UnityWebRequest.Result result;

        /// <summary>
        ///     <see cref="UnityWebRequest"/> 的回報碼
        /// </summary>
        public long reponseCode;

        /// <summary>
        ///     <see cref="UnityWebRequest"/> 的回報訊息
        /// </summary>
        public string message;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}