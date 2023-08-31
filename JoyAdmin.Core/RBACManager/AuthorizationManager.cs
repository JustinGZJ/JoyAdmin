using System.Linq;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Core;

/// <summary>
///     权限管理器
/// </summary>
public class AuthorizationManager : IAuthorizationManager, IScoped
{
    /// <summary>
    ///     请求上下文访问器
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IRepository<User> _userRepository;

    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="userRepository"></param>
    public AuthorizationManager(IHttpContextAccessor httpContextAccessor
        , IRepository<User> userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    /// <summary>
    ///     获取用户Id
    /// </summary>
    /// <returns></returns>
    public long GetUserId()
    {
        return long.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);
    }


    /// <summary>
    ///     获取用户名
    /// </summary>
    /// <returns></returns>
    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext.User.FindFirst("UserName").Value;
    }

    /// <summary>
    ///     检查权限
    /// </summary>
    /// <param name="resourceId"></param>
    /// <returns></returns>
    public bool CheckSecurity(string resourceId)
    {
        var userId = GetUserId();

        // ========= 以下代码应该缓存起来 ===========
        // 查询用户拥有的权限
        var securities = _userRepository
            .Include(u => u.Roles)
            .ThenInclude(u => u.Securities)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles
                .SelectMany(u => u.Securities))
            .Select(u => u.UniqueCode.ToLower());

        if (!securities.Contains(resourceId.ToLower())) return false;

        return true;
    }

    public bool CheckToken()
    {
        var bearerToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(bearerToken)) return false;
        // 获取 token
        var accessToken = bearerToken[7..];

        // 验证token
        var (IsValid, Token, validationResult) = JWTEncryption.Validate(accessToken);
        if (!IsValid) return false;
        return true;
    }
}