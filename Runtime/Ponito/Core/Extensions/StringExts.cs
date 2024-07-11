using System.Collections.Generic;
using System.Text;

namespace Ponito.Core.Extensions
{
    /// <summary>
    ///     字串系列延伸方法
    /// </summary>
    public static class StringExts
    {
        /// <summary>
        ///     防止浪費記憶體站存
        /// </summary>
        private static StringBuilder builder { get; } = new();
        
        /// <summary>
        ///     把 <see cref="char"/> 陣列轉成 <see cref="string"/> 字串
        /// </summary>
        public static string Collapse(this IEnumerable<char> chars)
        {
            builder.Clear();
            chars.ForEach(c => builder.Append(c));
            return builder.ToString();
        }
    }
}