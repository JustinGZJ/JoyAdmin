using JoyAdmin.Application.Dto;

namespace JoyAdmin.Application.RBAC.Dtos;

public class UserInput : PageDto
{
    /// <summary>
    ///     查询关键字
    /// </summary>
    public string keyword { get; set; }
}