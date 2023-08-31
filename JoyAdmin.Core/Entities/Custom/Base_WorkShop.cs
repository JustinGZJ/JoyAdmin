/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */

using System;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;

namespace JoyAdmin.Core.Entities.Custom;

public class Base_WorkShop : IEntity
{
    /// <summary>
    ///     车间主键
    /// </summary>
    [Key]
    [Display(Name = "车间主键")]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public Guid WorkShopId { get; set; }

    /// <summary>
    ///     车间名称
    /// </summary>
    [Display(Name = "车间名称")]
    [MaxLength(100)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string WorkShopName { get; set; }

    /// <summary>
    ///     车间编码
    /// </summary>
    [Display(Name = "车间编码")]
    [MaxLength(100)]
    [Editable(true)]
    public string WorkShopCode { get; set; }

    /// <summary>
    ///     负责人
    /// </summary>
    [Display(Name = "负责人")]
    [MaxLength(100)]
    [Editable(true)]
    public string MainPerson { get; set; }

    /// <summary>
    ///     面积
    /// </summary>
    [Display(Name = "面积")]
    [Editable(true)]
    public int? Area { get; set; }

    /// <summary>
    ///     是否启用
    /// </summary>
    [Display(Name = "是否启用")]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public int Enable { get; set; }

    /// <summary>
    ///     备注
    /// </summary>
    [Display(Name = "备注")]
    [MaxLength(1000)]
    [Editable(true)]
    public string Remark { get; set; }

    /// <summary>
    ///     创建人编号
    /// </summary>
    [Display(Name = "创建人编号")]
    [Editable(true)]
    public int? CreateID { get; set; }

    /// <summary>
    ///     创建人
    /// </summary>
    [Display(Name = "创建人")]
    [MaxLength(30)]
    [Editable(true)]
    public string Creator { get; set; }

    /// <summary>
    ///     创建时间
    /// </summary>
    [Display(Name = "创建时间")]
    [Editable(true)]
    public DateTime? CreateDate { get; set; }

    /// <summary>
    ///     修改人编号
    /// </summary>
    [Display(Name = "修改人编号")]
    [Editable(true)]
    public int? ModifyID { get; set; }

    /// <summary>
    ///     修改人
    /// </summary>
    [Display(Name = "修改人")]
    [MaxLength(30)]
    [Editable(true)]
    public string Modifier { get; set; }

    /// <summary>
    ///     修改时间
    /// </summary>
    [Display(Name = "修改时间")]
    [Editable(true)]
    public DateTime? ModifyDate { get; set; }
}