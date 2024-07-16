using System.IO;
using System.Linq;

namespace google_storage_upload_tool.Models
{
    /// <summary>
    ///     便利的路徑名稱資訊
    /// </summary>
    public readonly partial struct PathNameInfo
    {
        /// <summary>
        ///     用加的可以把兩者變成同一個路徑
        /// </summary>
        public static PathNameInfo operator +(PathNameInfo a, PathNameInfo b)
        {
            return new PathNameInfo(Path.Combine(a, b));
        }

        /// <summary>
        ///     變成字串
        /// </summary>
        /// <returns>路徑字串</returns>
        public override string ToString()
        {
            return names.Aggregate((a, b) => $"{a}/{b}");
        }

        /// <summary>
        ///     變成字串
        /// </summary>
        /// <param name="pathName">本路徑資料</param>
        /// <returns>字串</returns>
        public static implicit operator string(PathNameInfo pathName)
        {
            return pathName.ToString();
        }

        /// <summary>
        ///     把路徑合併到資料夾
        /// </summary>
        public PathNameInfo MergeTo(PathNameInfo folder)
        {
            var indexOf = names.IndexOf(folder[^1]);
            var trim    = new PathNameInfo(names.Skip(indexOf + 1));
            return folder + trim;
        }

        /// <summary>
        ///     檢查可不可以合併到資料夾
        /// </summary>
        public bool CanBeMergeTo(PathNameInfo folder)
        {
            var indexOf = names.IndexOf(folder[^1]);
            return indexOf > -1;
        }

        /// <summary>
        ///     是不是在資料夾之下的路徑呢？
        /// </summary>
        public bool IsRelativeTo(PathNameInfo folder)
        {
            if (folder.Length > Length) return false;

            for (var i = 0; i < folder.Length; i++)
            {
                if (names[i] == folder[i]) continue;
                return false;
            }

            return true;
        }
    }
}