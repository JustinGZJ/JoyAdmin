/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */

using System;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;

namespace JoyAdmin.Core.Entities.Custom;

public class Base_DefectItem : IEntity
{
    /// <summary>
    ///     不良品项表主键ID
    /// </summary>
    [Key]
    [Display(Name = "不良品项表主键ID")]
    [Required(AllowEmptyStrings = false)]
    public int DefectItem_Id { get; set; }

    /// <summary>
    ///     不良品项编号
    /// </summary>
    [Display(Name = "不良品项编号")]
    [Editable(true)]
    public string DefectItemCode { get; set; }

    /// <summary>
    ///     不良品项名称
    /// </summary>
    [Display(Name = "不良品项名称")]
    [MaxLength(200)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string DefectItemName { get; set; }

    /// <summary>
    ///     附件
    /// </summary>
    [Display(Name = "附件")]
    [Editable(true)]
    public string Attachment { get; set; }

    /// <summary>
    ///     图片
    /// </summary>
    [Display(Name = "图片")]
    [Editable(true)]
    public string ImageUrl { get; set; }

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
    public string Creator { get; set; }

    /// <summary>
    ///     修改人
    /// </summary>
    [Display(Name = "修改人")]
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
}