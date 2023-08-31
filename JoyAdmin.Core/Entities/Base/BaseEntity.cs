using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;

namespace JoyAdmin.Core;

/// <summary>
///     业务Entity基类
/// </summary>
public abstract class BaseEntity : IEntity
{
    /// <summary>
    ///     主键Id
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public virtual long Id { get; set; }

    /// <summary>
    ///     创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    /// <summary>
    ///     创建人Id
    /// </summary>
    public long CreatedUserId { get; set; }

    /// <summary>
    ///     创建人姓名
    /// </summary>
    [StringLength(50)]
    public string CreatedUser { get; set; }

    /// <summary>
    ///     修改时间
    /// </summary>
    public DateTime? ModifiedTime { get; set; }

    /// <summary>
    ///     修改人Id
    /// </summary>
    public long ModifiedUserId { get; set; }

    /// <summary>
    ///     修改人姓名
    /// </summary>
    [StringLength(50)]
    public string ModifiedUser { get; set; }


    /// <summary>
    ///     删除标志
    /// </summary>
    [DefaultValue(false)]
    public bool IsDeleted { get; set; }
}