using System.Collections.Generic;

namespace google_storage_upload_tool.Models
{
    public readonly partial struct PathNameInfo
    {
        /// <summary>
        ///     緩衝區大小
        /// </summary>
        private const int BUFFER_SIZE = 8192;

        /// <summary>
        ///     製作 <see cref="CreateCheckSum" /> 需要的緩衝區
        /// </summary>
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        /// <summary>
        ///     路徑分隔的各類名稱
        /// </summary>
        private readonly List<string> names;
    }
}