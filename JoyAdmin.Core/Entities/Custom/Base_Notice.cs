/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;

namespace JoyAdmin.Core.Entities.Custom;


public partial class Base_Notice:IEntity
{
    /// <summary>
    ///通知表主键ID
    /// </summary>
    [Key]
    [Display(Name ="通知表主键ID")]
    
    [Required(AllowEmptyStrings=false)]
    public int Notice_Id { get; set; }

    /// <summary>
    ///通知类型
    /// </summary>
    [Display(Name ="通知类型")]
    [MaxLength(200)]
    
    [Editable(true)]
    [Required(AllowEmptyStrings=false)]
    public string NoticeType { get; set; }

    /// <summary>
    ///标题
    /// </summary>
    [Display(Name ="标题")]
    [MaxLength(500)]
    
    [Editable(true)]
    [Required(AllowEmptyStrings=false)]
    public string NoticeTitle { get; set; }

    /// <summary>
    ///内容
    /// </summary>
    [Display(Name ="内容")]
    [MaxLength(4000)]
    
    [Editable(true)]
    [Required(AllowEmptyStrings=false)]
    public string NoticeContent { get; set; }

    /// <summary>
    ///创建时间
    /// </summary>
    [Display(Name ="创建时间")]
    
    public DateTime? CreateDate { get; set; }

    /// <summary>
    ///创建人编号
    /// </summary>
    [Display(Name ="创建人编号")]
    
    public int? CreateID { get; set; }

    /// <summary>
    ///创建人
    /// </summary>
    [Display(Name ="创建人")]
    [MaxLength(200)]
    
    public string Creator { get; set; }

    /// <summary>
    ///修改人
    /// </summary>
    [Display(Name ="修改人")]
    [MaxLength(200)]
    
    public string Modifier { get; set; }

    /// <summary>
    ///修改时间
    /// </summary>
    [Display(Name ="修改时间")]
    
    public DateTime? ModifyDate { get; set; }

    /// <summary>
    ///修改人编号
    /// </summary>
    [Display(Name ="修改人编号")]
    
    public int? ModifyID { get; set; }

       
}