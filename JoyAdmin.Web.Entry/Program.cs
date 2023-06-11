using System;
using Serilog;
using Serilog.Events;
using System.Text;
using Microsoft.Extensions.Hosting;

Serve.Run(RunOptions.Default
    .ConfigureBuilder(builder =>
    {
        builder.Host.UseSerilogDefault(config =>//默认集成了 控制台 和 文件 方式。如需自定义写入，则传入需要写入的介质即可：
        {
            string outputTemplate = "【时间】{Timestamp:yyyy-MM-dd HH:mm:ss,fff}{NewLine}【等级】{Level:u3}{NewLine}【消息】{Message:lj}{NewLine}{NewLine}";//输出模板
            config.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Debug)
                    .WriteTo.File($"logs/{LogEventLevel.Debug}/.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day, encoding: Encoding.UTF8))
                    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information)
                    .WriteTo.File($"logs/{LogEventLevel.Information}/.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day, encoding: Encoding.UTF8))
                    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning)
                    .WriteTo.File($"logs/{LogEventLevel.Warning}/.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day, encoding: Encoding.UTF8))
                    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)
                    .WriteTo.File($"logs/{LogEventLevel.Error}/.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day, encoding: Encoding.UTF8));

        });
    }));




 