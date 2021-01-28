using System;
using System.ComponentModel;
using static FileUpLoad.Utility.Enum;

namespace FileUpLoad.Utility
{

    /// <summary>
    /// 实体包装类
    /// </summary>
    [Serializable]
    public class ResultModel
    {
        #region 构造方法

        /// <summary>
        /// 
        /// </summary>
        public ResultModel() : this(null, ResultCodeEnum.Error)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public ResultModel(string message, ResultCodeEnum code)
        {
            Msg = message;
            Code = code;
        }
        #endregion

        /// <summary>
        /// 返回代码 
        /// </summary>
        [Description("返回代码")]
        public ResultCodeEnum Code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        [Description("返回消息")]
        public string Msg { get; set; }
    }

    /// <summary>
    /// 实体包装类
    /// </summary>
    [Serializable]
    public class ResultModel<T>
    {
        #region 构造方法

        /// <summary>
        /// 
        /// </summary>
        public ResultModel() : this(null, ResultCodeEnum.Error, default(T))
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="content"></param>
        public ResultModel(string message, ResultCodeEnum code, T content)
        {
            Msg = message;
            Code = code;
            Data = content;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public ResultModel<T> Success(string message, T content)
        {
            return new ResultModel<T>()
            {
                Msg = message,
                Code = ResultCodeEnum.Success,
                Data = content
            };
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public ResultModel<T> Error(string message, T content)
        {
            return new ResultModel<T>()
            {
                Msg = message,
                Code = ResultCodeEnum.Error,
                Data = content
            };
        }

        #endregion

        /// <summary>
        /// 返回代码 
        /// </summary>
        [Description("返回代码")]
        public ResultCodeEnum Code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        [Description("返回消息")]
        public string Msg { get; set; }
        /// <summary>
        /// 实体内容
        /// </summary>
        [Description("Result结果集")]
        public T Data { get; set; }
    }

}
