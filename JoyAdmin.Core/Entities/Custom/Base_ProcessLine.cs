/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;

namespace JoyAdmin.Core.Entities.Custom;

public class Base_ProcessLine : IEntity
{
    /// <summary>
    ///     工艺路线表主键ID
    /// </summary>
    [Key]
    [Display(Name = "工艺路线表主键ID")]
    [Required(AllowEmptyStrings = false)]
    public int ProcessLine_Id { get; set; }

    /// <summary>
    ///     工艺路线编号
    /// </summary>
    [Display(Name = "工艺路线编号")]
    [MaxLength(200)]
    [Editable(true)]
    public string ProcessLineCode { get; set; }

    /// <summary>
    ///     工艺路线名称
    /// </summary>
    [Display(Name = "工艺路线名称")]
    [MaxLength(200)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string ProcessLineName { get; set; }

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

    [Display(Name = "工序列表")]
    [ForeignKey("ProcessLine_Id")]
    public List<Base_ProcessLineList> Base_ProcessLineList { get; set; }
}