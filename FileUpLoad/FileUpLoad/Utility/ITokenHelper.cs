using System;
using System.Collections.Generic;
using static FileUpLoad.Utility.Enum;

namespace FileUpLoad.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITokenHelper
    {
        /// <summary>
        /// Token验证
        /// </summary>
        /// <param name="encodeJwt">token</param>
        /// <param name="secretKey">secretKey</param>
        /// <param name="validatePayLoad">自定义各类验证;是否包含那种申明，或者申明的值</param>
        /// <returns></returns>
        bool ValiToken(string encodeJwt, string secretKey, Func<Dictionary<string, string>, bool> validatePayLoad = null);

        /// <summary>
        /// 带返回状态的Token验证
        /// </summary>
        /// <param name="encodeJwt">token</param>
        /// <param name="secretKey">secretKey</param>
        /// <param name="validatePayLoad">自定义各类验证； 是否包含那种申明，或者申明的值</param>
        /// <param name="action"></param>
        /// <returns></returns>
        TokenType ValiTokenState(string encodeJwt, string secretKey, Func<Dictionary<string, string>, bool> validatePayLoad, Action<Dictionary<string, string>> action);
    }
}
