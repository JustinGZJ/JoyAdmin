using Furion.DatabaseAccessor;

namespace JoyAdmin.Core;

/// <summary>
///     用户和角色关系表
/// </summary>
public class RoleSecurity : IEntity
{
    /// <summary>
    ///     角色Id
    /// </summary>
    public long RoleId { get; set; }

    public Role Role { get; set; }

    /// <summary>
    ///     权限Id
    /// </summary>
    public long SecurityId { get; set; }

    public Security Security { get; set; }
}