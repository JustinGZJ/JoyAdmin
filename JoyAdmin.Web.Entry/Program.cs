using System;
using Serilog;
using Serilog.Events;
using System.Text;
using Microsoft.Extensions.Hosting;

Serve.Run(RunOptions.Default
    .ConfigureBuilder(builder =>
    {
        builder.Host.UseSerilogDefault(config =>//Ĭ�ϼ����� ����̨ �� �ļ� ��ʽ�������Զ���д�룬������Ҫд��Ľ��ʼ��ɣ�
        {
            string outputTemplate = "��ʱ�䡿{Timestamp:yyyy-MM-dd HH:mm:ss,fff}{NewLine}���ȼ���{Level:u3}{NewLine}����Ϣ��{Message:lj}{NewLine}{NewLine}";//���ģ��
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




 