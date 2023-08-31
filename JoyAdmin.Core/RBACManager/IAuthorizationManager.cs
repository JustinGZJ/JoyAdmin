namespace JoyAdmin.Core;

/// <summary>
///     权限管理器
/// </summary>
public interface IAuthorizationManager
{
    /// <summary>
    ///     获取用户 Id
    /// </summary>
    /// <returns></returns>
    long GetUserId();

    /// <summary>
    ///     获取用户名称
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    string GetUserName();

    /// <summary>
    ///     检查授权
    /// </summary>
    /// <param name="resourceId"></param>
    /// <returns></returns>
    bool CheckSecurity(string resourceId);
}