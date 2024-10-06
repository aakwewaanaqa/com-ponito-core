using System;
using System.IO;

namespace Ponito.Core.Paths
{
    public readonly partial struct PathNameInfo
    {
        /// <summary>
        ///     取得元素名稱
        /// </summary>
        public string this[int index] => names[index];

        /// <summary>
        ///     取得元素名稱
        /// </summary>
        public string this[Index index] => names[index];

        /// <summary>
        ///     取得元素名稱
        /// </summary>
        public PathNameInfo this[Range range]
        {
            get
            {
                var tuple = range.GetOffsetAndLength(Length);
                return new PathNameInfo(names.GetRange(tuple.Offset, tuple.Length));
            }
        }

        /// <summary>
        ///     路徑總名稱長度
        /// </summary>
        public int Length => names.Count;

        /// <summary>
        ///     以檔案位置方式輸出
        /// </summary>
        public FileInfo File => new(ToString());

        /// <summary>
        ///     以路徑方式輸出
        /// </summary>
        public DirectoryInfo Directory => new DirectoryInfo(this[..1]);
    }
}