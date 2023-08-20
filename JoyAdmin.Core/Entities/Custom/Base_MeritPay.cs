/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;

namespace JoyAdmin.Core.Entities.Custom;


public partial class Base_MeritPay:IEntity
{
    /// <summary>
    ///绩效工资配置主键ID
    /// </summary>
    [Key]
    [Display(Name ="绩效工资配置主键ID")]
   
    [Required(AllowEmptyStrings=false)]
    public int MeritPay_Id { get; set; }

    /// <summary>
    ///工序名称
    /// </summary>
    [Display(Name ="工序名称")]
   
    [Editable(true)]
    public int? Process_Id { get; set; }

    /// <summary>
    ///报工单位
    /// </summary>
    [Display(Name ="报工单位")]
   
    [Editable(true)]
    public int? Unit_Id { get; set; }

    /// <summary>
    ///产品名称
    /// </summary>
    [Display(Name ="产品名称")]
   
    [Editable(true)]
    public int? Product_Id { get; set; }

    /// <summary>
    ///工资单价(元)
    /// </summary>
    [Display(Name ="工资单价(元)")]
    [DisplayFormat(DataFormatString="20,3")]
   
    [Editable(true)]
    [Required(AllowEmptyStrings=false)]
    public decimal UnitPrice { get; set; }

    /// <summary>
    ///标准效率
    /// </summary>
    [Display(Name ="标准效率")]
   
    [Editable(true)]
    public int? StandardNumber { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name ="StandardHour")]
   
    [Editable(true)]
    public int? StandardHour { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name ="StandardMin")]
   
    [Editable(true)]
    public int? StandardMin { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name ="StandardSec")]
   
    [Editable(true)]
    public int? StandardSec { get; set; }

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