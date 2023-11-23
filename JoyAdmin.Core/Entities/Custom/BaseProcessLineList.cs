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

public class BaseProcessLineList : IEntity, IEntityTypeBuilder<BaseProcessLineList>
{
    /// <summary>
    ///     工艺路线工序列表主键ID
    /// </summary>
    [Key]
    [Display(Name = "工艺路线工序列表主键ID")]
    [Required(AllowEmptyStrings = false)]
    public int ProcessLineListId { get; set; }

    /// <summary>
    ///     工艺路线
    /// </summary>
    [Display(Name = "工艺路线")]
    public int? ProcessLineId { get; set; }

    public Base_ProcessLine ProcessLine { get; set; }

    /// <summary>
    ///     类型
    /// </summary>
    [Display(Name = "类型")]
    [MaxLength(200)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string ProcessLineType { get; set; }

    /// <summary>
    ///     工序
    /// </summary>
    [Display(Name = "工序")]
    [Editable(true)]
    public int? Process_Id { get; set; }

    public Base_Process Process { get; set; }

    /// <summary>
    ///     工艺路线
    /// </summary>
    [Display(Name = "工艺路线")]
    [Editable(true)]
    public int? ProcessLineDown_Id { get; set; }
    
    public Base_ProcessLine ProcessLineDown { get; set; }

    /// <summary>
    ///     顺序
    /// </summary>
    [Display(Name = "顺序")]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public int Sequence { get; set; }

    /// <summary>
    ///     报工数配比
    /// </summary>
    [Display(Name = "报工数配比")]
    [DisplayFormat(DataFormatString = "20,3")]
    [Editable(true)]
    public decimal? SubmitWorkMatch { get; set; }

    /// <summary>
    ///     创建时间
    /// </summary>
    [Display(Name = "创建时间")]
    public DateTime? CreateDate { get; set; }

    /// <summary>
    ///     创建人编号
    /// </summary>
    [Display(Name = "创建人编号")]
    public int? CreateID { get; set; }

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
    public int? ModifyID { get; set; }

    public void Configure(EntityTypeBuilder<BaseProcessLineList> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
      //  throw new NotImplementedException();
      entityBuilder.HasOne(x=>x.ProcessLineDown)
          .WithMany().HasForeignKey(x=>x.ProcessLineDown_Id);
      entityBuilder.HasOne(x=>x.Process)
          .WithMany().HasForeignKey(x=>x.Process_Id);
      
    }
}