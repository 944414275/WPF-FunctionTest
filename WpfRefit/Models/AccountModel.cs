using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRefit.Models
{
    public class AccountModel
    {
        /// <summary>
        /// 
        /// </summary>
        public AccountModel()
        {
            Id = new Random().Next(1, 100000);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
