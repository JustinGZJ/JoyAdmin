namespace JoyAdmin.Application.RBAC.Dtos;

public class SecurityDto
{
    /// <summary>
    /// id
    /// </summary>
    public long Id { get; set; }
        
    /// <summary>
    /// 唯一权限编码
    /// </summary>
    public string UniqueCode { get; set; }

    /// <summary>
    /// 权限唯一名 
    /// </summary>
    public string UniqueName { get; set; }

    public int Sort { get; set; }
}