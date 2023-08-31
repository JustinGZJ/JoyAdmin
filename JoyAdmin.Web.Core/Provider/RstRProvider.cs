using System;
using System.Threading.Tasks;
using Furion;
using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.UnifyResult;
using Furion.UnifyResult.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JoyAdmin.Web.Core.Provider;

[SuppressSniffer]
[UnifyModel(typeof(RESTfulResult<>))]
public class RstRProvider : IUnifyResultProvider
{
    public IActionResult OnException(ExceptionContext context, ExceptionMetadata metadata)
    {
        return new JsonResult(new RESTfulResult<object>
        {
            StatusCode = metadata.StatusCode,
            Succeeded = false,
            Data = null,
            Errors = metadata.Errors,
            Extras = UnifyContext.Take(),
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        });
    }


    /// <summary>
    ///     处理输出状态码
    /// </summary>
    /// <param name="context"></param>
    /// <param name="statusCode"></param>
    /// <param name="unifyResultSettings"></param>
    /// <returns></returns>
    public async Task OnResponseStatusCodes(HttpContext context, int statusCode,
        UnifyResultSettingsOptions unifyResultSettings)
    {
        // 设置响应状态码
        UnifyContext.SetResponseStatusCodes(context, statusCode, unifyResultSettings);

        switch (statusCode)
        {
            // 处理 401 状态码
            case StatusCodes.Status401Unauthorized:
                await context.Response.WriteAsJsonAsync(new RESTfulResult<object>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Succeeded = false,
                    Data = null,
                    Errors = "401 Unauthorized",
                    Extras = UnifyContext.Take(),
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                }, App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                break;
            // 处理 403 状态码
            case StatusCodes.Status403Forbidden:
                await context.Response.WriteAsJsonAsync(new RESTfulResult<object>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Succeeded = false,
                    Data = null,
                    Errors = "403 Forbidden",
                    Extras = UnifyContext.Take(),
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                }, App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                break;
        }
    }

    public IActionResult OnSucceeded(ActionExecutedContext context, object data)
    {
        // 处理内容结果
        if (context.Result is ContentResult contentResult) data = contentResult.Content;
        // 处理对象结果
        else if (context.Result is ObjectResult objectResult) data = objectResult.Value;
        else if (context.Result is EmptyResult) data = null;
        else return null;

        return new JsonResult(new RESTfulResult<object>
        {
            StatusCode = StatusCodes.Status200OK,
            Succeeded = true,
            Data = data,
            Errors = null,
            Extras = UnifyContext.Take(),
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        });
    }

    public IActionResult OnValidateFailed(ActionExecutingContext context, ValidationMetadata metadata)
    {
        return new JsonResult(new RESTfulResult<object>
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Succeeded = false,
            Data = null,
            Errors = metadata.Message,
            Extras = UnifyContext.Take(),
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        });
    }
}