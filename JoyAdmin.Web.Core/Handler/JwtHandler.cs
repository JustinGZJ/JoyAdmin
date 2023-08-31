using System.Threading.Tasks;
using Furion;
using Furion.Authorization;
using JoyAdmin.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace JoyAdmin.Web.Core;

public class JwtHandler : AppAuthorizeHandler
{
    /// <summary>
    ///     请求管道
    /// </summary>
    /// <param name="context"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 此处已经自动验证 Jwt token的有效性了，无需手动验证

        // 检查权限，如果方法时异步的就不用 Task.FromResult 包裹，直接使用 async/await 即可
        return Task.FromResult(CheckAuthorzie(httpContext));
    }

    /// <summary>
    ///     检查权限
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private static bool CheckAuthorzie(DefaultHttpContext httpContext)
    {
        // 获取权限特性
        var securityDefineAttribute = httpContext.GetMetadata<SecurityDefineAttribute>();
        if (securityDefineAttribute == null) return true;

        return App.GetService<IAuthorizationManager>().CheckSecurity(securityDefineAttribute.ResourceId);
    }
}