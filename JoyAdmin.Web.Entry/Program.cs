using System.Text;
using Serilog;
using Serilog.Events;

Serve.Run(RunOptions.Default
    .ConfigureBuilder(builder =>
    {
        builder.Host.UseSerilogDefault(config => //???????? ????? ?? ??? ??????????????§Õ?????????§Õ??????????
        {
            var outputTemplate =
                "?????{Timestamp:yyyy-MM-dd HH:mm:ss,fff}{NewLine}???????{Level:u3}{NewLine}???????{Message:lj}{NewLine}{NewLine}"; //??????
            config.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Debug)
                    .WriteTo.File($"logs/{LogEventLevel.Debug}/.log", outputTemplate: outputTemplate,
                        rollingInterval: RollingInterval.Day, encoding: Encoding.UTF8))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information)
                    .WriteTo.File($"logs/{LogEventLevel.Information}/.log", outputTemplate: outputTemplate,
                        rollingInterval: RollingInterval.Day, encoding: Encoding.UTF8))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning)
                    .WriteTo.File($"logs/{LogEventLevel.Warning}/.log", outputTemplate: outputTemplate,
                        rollingInterval: RollingInterval.Day, encoding: Encoding.UTF8))
                .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)
                    .WriteTo.File($"logs/{LogEventLevel.Error}/.log", outputTemplate: outputTemplate,
                        rollingInterval: RollingInterval.Day, encoding: Encoding.UTF8));
        });
    }));