namespace JoyAdmin.Application.RBAC.Dtos;

public class RoleSecurityInput
{
    public long RoleId { get; set; }

    public long[] SecurityIds { get; set; }
}