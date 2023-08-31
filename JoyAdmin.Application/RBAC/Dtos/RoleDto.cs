namespace JoyAdmin.Application.RBAC.Dtos;

public class RoleDto
{
    public long Id { get; set; }

    /// <summary>
    ///     角色名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     角色描述
    /// </summary>
    public string Remark { get; set; }

    public int Sort { get; set; }
}