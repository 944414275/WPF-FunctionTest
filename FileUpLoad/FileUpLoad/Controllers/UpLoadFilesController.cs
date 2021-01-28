using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileUpLoad.Filter;
using FileUpLoad.Utility;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static FileUpLoad.Utility.Enum;

namespace FileUpLoad.Controllers
{
    /// <summary>
    /// 文件上传
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UpLoadFilesController : ControllerBase
    { 
        private readonly UpFileOptions _upFileOptions;
        private readonly ITimeLimitedDataProtector _protector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="upFileOptions"></param>
        /// <param name="provider"></param>
        public UpLoadFilesController(IOptions<UpFileOptions> upFileOptions
            , IDataProtectionProvider provider)
        {
            _upFileOptions = upFileOptions.Value;
            _protector = provider.CreateProtector("fileProtector").ToTimeLimitedDataProtector();
        }

        /// <summary>
        /// 普通文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(TokenFilter))]
        [RequestSizeLimit(100_000_000)] //最大100m左右
        public async Task<ResultModel<string>> UpLoadFiles(IFormFile file)
        {
            //加[FromForm] IFormFile file的特性，可以看清楚接口对象里面的详细参数
            var result = new ResultModel<string>();
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("id").Value);
            try
            {
                return await UpLoadHelper.UploadWriteFileAsync(file, _upFileOptions, userId);
            }
            catch (Exception ex)
            {
                return result.Error("上传异常,原因:" + ex.Message, "");
            }
        }

        #region 文件下载

        /// <summary>
        /// 根据文件名获取文件秘钥
        /// </summary>
        /// <param name="url">下载地址(相对路径)</param>
        /// <returns></returns>
        [ServiceFilter(typeof(TokenFilter))]
        [HttpGet]
        public ResultModel<string> GetDownLoadFile(string url)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("id").Value);
            //根据orderNo 与 会员Id查询 订单信息  

            /*
             此时为了跟大家演示 下载效果 只需要传参 url即可
             真实的业务场景，会根据登录用户，以及业务编号  我们可以根据需求去重写 对应的业务即可即可
             */

            var result = new ResultModel<string>();
            if (string.IsNullOrEmpty(url))
                return result.Error("请输入文件地址", null);
            string cdipath = Directory.GetCurrentDirectory() + url;
            if (!System.IO.File.Exists(cdipath))
                return result.Error("文件不存在", null);
            //根据当前登录用户验证 文件相对路径 是否包含userId值
            if (!url.Contains(userId.ToString() + "_"))
                return result.Error("您当前下载的文件不正确", null);

            /*
             真实场景  下载客户文件时，需要根据业务信息动态获取客户源文件名，生成文件名的规则 你们也可以自定义
             此处 我会根据切割  地址获取文件名 /MyUpfiles/2020/09/24/55020_04-03-{文件名}_c825733e-3f87-44b5-b002-0fd4d94a94d1.zip
           */
            var getfileName = url.Split('_')[1].ToString();
            var fileKey = _protector.Protect(getfileName, TimeSpan.FromSeconds(120));
            //fileKey 是根据文件进行加密后的秘钥，有效时间为：120s
            return result.Success("文件秘钥生成成功", fileKey);
        }

        /// <summary>
        /// 根据文件秘钥 以及订单编号 实现文件下载
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetRawFile(string fileKey, string url)
        {
            try
            {
                /*
               文件下载时，我会先根据 文件路径 创建一个文件key，然后设置他的过期时间
               当前为了跟大家演示效果，为了读取 加密的url，所有 将url进行传入，
               真实业务场景，会传入一个 订单编号 或者业务编号去动态查询 上传地址
            */

                //通过订单编号-orderNo 查询客户上传的文件路径  
                var rawFileInfo = _protector.Unprotect(fileKey);
                string cdipath = Directory.GetCurrentDirectory() + url;
                var stream = System.IO.File.OpenRead(cdipath);
                stream.Position = 0;
                return File(stream, "application/oct-stream", rawFileInfo);
            }
            catch
            {
                return StatusCode(401);
            }
        } 
        #endregion 
    }
}