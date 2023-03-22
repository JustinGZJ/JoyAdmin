using JoyAdmin.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JoyAdmin.Application
{
    /// <summary>
    /// 登录输出参数
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(50)]
        public string Account { get; set; }
 
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [StringLength(50)]
        public string Phone { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [StringLength(50)]
        public string LinkPost { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Access { get; set; } = new List<string>();
    }
}