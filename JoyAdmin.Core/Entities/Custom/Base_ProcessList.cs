/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */

using System;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoyAdmin.Core.Entities.Custom;

public class BaseProcessList : IEntity, IEntityTypeBuilder<BaseProcessList>
{
    public enum MyEnum
    {
        
    }
    /// <summary>
    ///     工序采集数据主键ID
    /// </summary>
    [Key]
    [Display(Name = "工序采集数据主键ID")]
    [Required(AllowEmptyStrings = false)]
    public int ProcessList_Id { get; set; }

    /// <summary>
    ///     工序ID
    /// </summary>
    [Display(Name = "工序ID")]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public int? Process_Id { get; set; }

    public Base_Process Process { get; set; }


    /// <summary>
    ///     类型
    /// </summary>
    [Display(Name = "类型")]
    [MaxLength(200)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string DataPointType { get; set; }

    /// <summary>
    ///     名称
    /// </summary>
    [Display(Name = "名称")]
    [MaxLength(200)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string DataPointName { get; set; }

    /// <summary>
    ///     创建时间
    /// </summary>
    [Display(Name = "创建时间")]
    public DateTime? CreateDate { get; set; }

    /// <summary>
    ///     创建人编号
    /// </summary>
    [Display(Name = "创建人编号")]
    public int? CreateId { get; set; }

    /// <summary>
    ///     创建人
    /// </summary>
    [Display(Name = "创建人")]
    [MaxLength(200)]

    public string Creator { get; set; }

    /// <summary>
    ///     修改人
    /// </summary>
    [Display(Name = "修改人")]
    [MaxLength(200)]

    public string Modifier { get; set; }

    /// <summary>
    ///     修改时间
    /// </summary>
    [Display(Name = "修改时间")]
    public DateTime? ModifyDate { get; set; }

    /// <summary>
    ///     修改人编号
    /// </summary>
    [Display(Name = "修改人编号")]
    public int? ModifyId { get; set; }

    public void Configure(EntityTypeBuilder<BaseProcessList> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.ToTable("Base_ProcessList");

        entityBuilder.HasKey(k => k.ProcessList_Id);

        entityBuilder.HasOne(x => x.Process)
            .WithMany().HasForeignKey(x => x.Process_Id).IsRequired(false);
        entityBuilder.Property(x => x.DataPointType).IsRequired();
    }
}