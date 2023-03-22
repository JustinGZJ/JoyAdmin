using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JoyAdmin.Core
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Security : BaseEntity, IEntitySeedData<Security>
    {
        /// <summary>
        /// 权限唯一编码
        /// </summary>
        [StringLength(50)]
        public string UniqueCode { get; set; }
        /// <summary>
        /// 权限唯一名（每一个接口）
        /// </summary>
        [StringLength(50)]
        public string UniqueName { get; set; }

        /// <summary>
        /// 排序  
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<RoleSecurity> RoleSecurities { get; set; }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Security> HasData(DbContext dbContext, Type dbContextLocator)
        {
            
            var list = new List<Security>
            { 
                new Security { Id = 1, CreatedTime = DateTime.Now,  UniqueCode = "role",UniqueName = "角色管理"  },
                new Security { Id = 2, CreatedTime = DateTime.Now, UniqueCode = "auth",UniqueName = "权限管理"   },
                new Security { Id = 3, CreatedTime = DateTime.Now,  UniqueCode = "employee",UniqueName = "员工管理" }
            };
            
            return list;
        }
    }
}