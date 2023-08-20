using System.ComponentModel.DataAnnotations;

namespace JoyAdmin.Application.RBAC.Dtos;

/// <summary>
/// 登录输入参数
/// </summary>
public class UpdatePwdInput
{

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "原密码不能为空")]
    public string OldPassword { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    public string Password { get; set; }
}