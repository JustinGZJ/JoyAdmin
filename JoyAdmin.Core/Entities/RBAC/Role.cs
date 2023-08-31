using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoyAdmin.Core;

/// <summary>
///     角色表
/// </summary>
public class Role : BaseEntity, IEntitySeedData<Role>, IEntityTypeBuilder<Role>
{
    /// <summary>
    ///     角色名称
    /// </summary>
    [StringLength(50)]
    public string Name { get; set; }

    /// <summary>
    ///     角色描述
    /// </summary>
    [StringLength(200)]
    public string Remark { get; set; }

    /// <summary>
    ///     排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    ///     多对多
    /// </summary>
    public ICollection<User> Users { get; set; }

    /// <summary>
    ///     多对多中间表
    /// </summary>
    public List<UserRole> UserRoles { get; set; }

    /// <summary>
    ///     多对多
    /// </summary>
    public ICollection<Security> Securities { get; set; }

    /// <summary>
    ///     多对多中间表
    /// </summary>
    public List<RoleSecurity> RoleSecurities { get; set; }

    /// <summary>
    ///     种子数据
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    /// <returns></returns>
    public IEnumerable<Role> HasData(DbContext dbContext, Type dbContextLocator)
    {
        return new[]
        {
            new Role
            {
                Id = 1, CreatedTime = DateTime.Now, Name = "管理员", Remark = "拥有系统管理权限"
            }
        };
    }

    /// <summary>
    ///     配置多对多关系
    /// </summary>
    /// <param name="entityBuilder"></param>
    /// <param name="dbContext"></param>
    /// <param name="dbContextLocator"></param>
    public void Configure(EntityTypeBuilder<Role> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.HasMany(p => p.Securities)
            .WithMany(p => p.Roles)
            .UsingEntity<RoleSecurity>(
                u => u.HasOne(c => c.Security).WithMany(c => c.RoleSecurities).HasForeignKey(c => c.SecurityId)
                , u => u.HasOne(c => c.Role).WithMany(c => c.RoleSecurities).HasForeignKey(c => c.RoleId)
                , u =>
                {
                    u.HasKey(c => new { c.RoleId, c.SecurityId });
                    // 添加多对多种子数据
                    u.HasData(
                        new { CreatedTime = DateTime.Now, RoleId = 1L, SecurityId = 1L },
                        new { CreatedTime = DateTime.Now, RoleId = 1L, SecurityId = 2L },
                        new { CreatedTime = DateTime.Now, RoleId = 1L, SecurityId = 3L }
                    );
                });
    }
}