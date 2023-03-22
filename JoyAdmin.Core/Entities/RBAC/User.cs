using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JoyAdmin.Core
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User : BaseEntity, IEntitySeedData<User>, IEntityTypeBuilder<User>
    {

       
        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(50)]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(50)]
        public string Password { get; set; }
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
        /// 多对多
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 配置多对多关系
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<User> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasMany(p => p.Roles)
                 .WithMany(p => p.Users)
                 .UsingEntity<UserRole>(
                   u => u.HasOne(c => c.Role).WithMany(c => c.UserRoles).HasForeignKey(c => c.RoleId)
                 , u => u.HasOne(c => c.User).WithMany(c => c.UserRoles).HasForeignKey(c => c.UserId)
                 , u =>
                 {
                     u.HasKey(c => new { c.UserId, c.RoleId });
                     u.HasData(new { UserId = 1L, RoleId = 1L });
                 });
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<User> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {
                new User
                {
                    Id=1,CreatedTime=DateTime.Now,Account="admin",Password="c33367701511b4f6020ec61ded352059"
                } 
                
            };
        }
    }
}