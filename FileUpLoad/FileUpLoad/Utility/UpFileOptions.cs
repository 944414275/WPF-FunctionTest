using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpLoad.Utility
{
    /// <summary>
    /// 文件上传类型
    /// </summary>
    public class UpFileOptions
    {
        /// <summary>
        /// 允许的文件类型
        /// </summary>
        public string FileTypes { get; set; }

        /// <summary>
        /// 最大文件大小
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// 上传文件路径
        /// </summary>
        public string UploadFilePath { get; set; }
    }
}
