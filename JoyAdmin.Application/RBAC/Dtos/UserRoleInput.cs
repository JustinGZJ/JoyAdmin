namespace JoyAdmin.Application.RBAC.Dtos;

public class UserRoleInput
{
    public long UserId { get; set; }

    public long[] RoleIds { get; set; }
}