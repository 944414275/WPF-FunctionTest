using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileUpLoad.Utility
{
    /// <summary>
    ///  文件上传 帮助类
    /// </summary>
    public static class UpLoadHelper
    {
        /// <summary>
        /// 上传文件，配置信息从自定义的json文件中拿取
        /// </summary>
        /// <param name="file"></param>
        /// <param name="upFileOptions">文件上传所需配置参数</param>
        /// <param name="userId">会员编号</param>
        /// <returns></returns>
        public static async Task<ResultModel<string>> UploadWriteFileAsync(IFormFile file, UpFileOptions upFileOptions, int userId)
        {
            var result = new ResultModel<string>();
            try
            {
                if (file != null && file.Length > 0)
                {
                    if (file.Length <= upFileOptions.MaxSize)//检查文件大小
                    {
                        var suffix = Path.GetExtension(file.FileName);//提取上传的文件文件后缀
                        if (upFileOptions.FileTypes.IndexOf(suffix) >= 0)//检查文件格式
                        {
                            string contentType = file.ContentType;
                            //原始的文件名称
                            string fileOrginname = file.FileName;
                            //判断文件的格式是否正确
                            string fileExtention = Path.GetExtension(fileOrginname);
                            string cdipath = Directory.GetCurrentDirectory();

                            //新的文件名组合规则 userId+原始文件名+Guid+扩展名
                            string fileupName = userId.ToString() + "_" + fileOrginname.Split(".")[0].ToString() + "_" + Guid.NewGuid().ToString("d") + fileExtention;
                            //相对路径
                            var relativePath = upFileOptions.UploadFilePath + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd");
                            string upfileDirePath = cdipath + relativePath;
                            var upfilePath = Path.Combine(upfileDirePath, fileupName);
                            if (!Directory.Exists(upfileDirePath))
                                Directory.CreateDirectory(upfileDirePath);
                            if (!File.Exists(upfilePath))
                            {
                                using var Stream = File.Create(upfilePath);
                            }
                            using (FileStream fileStream = new FileStream(upfilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, upFileOptions.MaxSize))
                            {
                                await file.CopyToAsync(fileStream);
                                return result.Success("上传成功", relativePath + "/" + fileupName);
                            }
                        }
                        else
                        {
                            return result.Error("不支持此文件类型", "");
                        }
                    }
                    else
                    {
                        return result.Error($"文件大小不得超过{upFileOptions.MaxSize / (1024f * 1024f)}M", "");
                    }
                }
                else
                {
                    return result.Error("请选择需要上传的文件", "");
                }
            }
            catch (Exception ex)
            {
                return result.Error("发生异常" + ex.Message, "");
            }
        }

    }
}
