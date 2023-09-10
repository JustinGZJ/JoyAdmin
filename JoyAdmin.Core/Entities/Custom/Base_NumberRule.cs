/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */

using System;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;

namespace JoyAdmin.Core.Entities.Custom;

public class Base_NumberRule : IEntity
{
    /// <summary>
    ///     自定义编号规则主键ID
    /// </summary>
    [Key]
    [Display(Name = "自定义编号规则主键ID")]
    [Required(AllowEmptyStrings = false)]
    public int NumberRule_Id { get; set; }

    /// <summary>
    ///     目标表单
    /// </summary>
    [Display(Name = "目标表单")]
    [MaxLength(200)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string FormCode { get; set; }

    /// <summary>
    ///     编号前缀
    /// </summary>
    [Display(Name = "编号前缀")]
    [MaxLength(200)]
    [Editable(true)]
    public string Prefix { get; set; }

    /// <summary>
    ///     时间格式 可以是yyyyMMddHHmmss yyyyMM  yyyyMMdd yyyyMMddHHmmssfff
    /// </summary>
    [Display(Name = "时间格式")]
    [MaxLength(200)]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public string DateRule { get; set; }

    /// <summary>
    ///     流水号长度
    /// </summary>
    [Display(Name = "流水号长度")]
    [Editable(true)]
    [Required(AllowEmptyStrings = false)]
    public int SNLength { get; set; }

    /// <summary>
    ///     编号生成规则
    /// </summary>
    [Display(Name = "编号生成规则")]
    [MaxLength(200)]

    [Required(AllowEmptyStrings = false)]
    public string GenerativeRule { get; set; }
    /// <summary>
    ///   备注
    /// </summary>
    public string Remark { get; set; }

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
}