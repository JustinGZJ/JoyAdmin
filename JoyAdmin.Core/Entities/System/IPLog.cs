using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoyAdmin.Core 
{
    public class IPLog: BaseEntity
    {
        /// <summary>
        /// 登录ip
        /// </summary>
        [StringLength(50)]
        public string Ip { get; set; }
        /// <summary>
        /// 登录真实ip
        /// </summary>
        [StringLength(50)]
        public string RealIp { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        [StringLength(200)]
        public string UserAgent { get; set; }
        /// <summary>
        /// Referer
        /// </summary>
        [StringLength(200)]
        public string Referer { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [StringLength(50)]
        public long UserId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [StringLength(50)]
        public string UserAccount { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [StringLength(50)]
        public string UserName { get; set; }
 

    }
}
