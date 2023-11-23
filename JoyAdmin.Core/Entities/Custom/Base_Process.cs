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

public class Base_Process : IEntity
{
    /// <summary>
    ///     工序主键ID
    /// </summary>
    [Key]
    [Display(Name = "工序主键ID")]
    [Required(AllowEmptyStrings = false)]
    public int Process_Id { get; set; }

    /// <summary>
    ///     工序编号
    /// </summary>
    [Display(Name = "工序编号")]
    [MaxLength(200)]
    [Editable(true)]
    public string ProcessCode { get; set; }

    /// <summary>
    ///     工序名称
    /// </summary>
    [Display(Name = "工序名称")]
    [MaxLength(200)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string ProcessName { get; set; }

    /// <summary>
    ///     报工权限
    /// </summary>
    [Display(Name = "报工权限")]
    [MaxLength(1000)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string SubmitWorkLimit { get; set; }

    /// <summary>
    ///     报工数配比
    /// </summary>
    [Display(Name = "报工数配比")]
    [DisplayFormat(DataFormatString = "20,3")]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public decimal SubmitWorkMatch { get; set; }

    /// <summary>
    ///     不良品项列表
    /// </summary>
    [Display(Name = "不良品项列表")]
    [MaxLength(1000)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string DefectItem { get; set; }

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

    [Display(Name = "工序采集数据")]
    public List<BaseProcessList> Base_ProcessList { get; set; }
}