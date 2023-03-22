using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoyAdmin.Web.Core
{
    public class LogExceptionHandler : IGlobalExceptionHandler, ISingleton
    {
        ILogger _logger;
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
}
