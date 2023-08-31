/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */

using System;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;

namespace JoyAdmin.Core.Entities.Custom;

public class Base_DesktopMenu : IEntity
{
    /// <summary>
    ///     主页菜单表主键ID
    /// </summary>
    [Key]
    [Display(Name = "主页菜单表主键ID")]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public int DesktopMenu_Id { get; set; }

    /// <summary>
    ///     菜单
    /// </summary>
    [Display(Name = "菜单")]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public int MenuId { get; set; }

    /// <summary>
    ///     菜单名称
    /// </summary>
    [Display(Name = "菜单名称")]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string MenuName { get; set; }

    /// <summary>
    ///     路由地址
    /// </summary>
    [Display(Name = "路由地址")]
    [MaxLength(100)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string MenuUrl { get; set; }

    /// <summary>
    ///     背景颜色
    /// </summary>
    [Display(Name = "背景颜色")]
    [MaxLength(100)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string Color { get; set; }

    /// <summary>
    ///     是否启用
    /// </summary>
    [Display(Name = "是否启用")]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public byte Enable { get; set; }

    /// <summary>
    ///     创建时间
    /// </summary>
    [Display(Name = "创建时间")]
    [Editable(true)]
    public DateTime? CreateDate { get; set; }

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
    [MaxLength(200)]
    [Editable(true)]
    public string Creator { get; set; }

    /// <summary>
    ///     修改人
    /// </summary>
    [Display(Name = "修改人")]
    [MaxLength(200)]
    [Editable(true)]
    public string Modifier { get; set; }

    /// <summary>
    ///     修改时间
    /// </summary>
    [Display(Name = "修改时间")]
    [Editable(true)]
    public DateTime? ModifyDate { get; set; }

    /// <summary>
    ///     修改人编号
    /// </summary>
    [Display(Name = "修改人编号")]
    [Editable(true)]
    public int? ModifyID { get; set; }
}