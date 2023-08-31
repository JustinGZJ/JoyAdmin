using System.Threading.Tasks;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace JoyAdmin.Web.Core;

public class LogExceptionHandler : IGlobalExceptionHandler, ISingleton
{
    private readonly ILogger _logger;

    public LogExceptionHandler(ILogger logger)
    {
        _logger = logger;
    }

    public Task OnExceptionAsync(ExceptionContext context)
    {
        //记录日志
        _logger.Error(context.Exception.ToString());
        return Task.CompletedTask;
    }
}