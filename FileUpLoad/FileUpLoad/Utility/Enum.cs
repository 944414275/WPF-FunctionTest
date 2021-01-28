using System.ComponentModel;

namespace FileUpLoad.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class Enum
    {
        /// <summary>
        /// 系统数据返回状态
        /// </summary>
        public enum ResultCodeEnum
        {
            /// <summary>
            /// 失败
            /// </summary>
            [Description("失败")]
            Error = 0,

            /// <summary>
            /// 成功
            /// </summary>
            [Description("成功")]
            Success = 1,

            /// <summary>
            /// 接口未授权
            /// </summary>
            [Description("接口未授权")]
            ApiUnauthorized = 401

        }

        /// <summary>
        /// 
        /// </summary>
        public enum TokenType
        {
            /// <summary>
            /// 验证成功
            /// </summary>
            [Description("验证成功")]
            Ok,

            /// <summary>
            /// 验证失败
            /// </summary>
            [Description("验证失败")]
            Fail,

            /// <summary>
            /// Token失效
            /// </summary>
            [Description("Token失效")]
            Expired,

            /// <summary>
            /// Token非法
            /// </summary>
            [Description("Token非法")]
            FormError
        }
    }
}
