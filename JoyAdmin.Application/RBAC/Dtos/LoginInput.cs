using System.ComponentModel.DataAnnotations;

namespace JoyAdmin.Application.RBAC.Dtos;

/// <summary>
///     登录输入参数
/// </summary>
public class LoginInput
{
    /// <summary>
    ///     用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空")]
    public string Account { get; set; }

    /// <summary>
    ///     密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    public string Password { get; set; }
}