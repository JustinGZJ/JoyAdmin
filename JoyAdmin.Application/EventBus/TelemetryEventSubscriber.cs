using System;
using System.Text.Json;
using System.Threading.Tasks;
using Furion.ClayObject.Extensions;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.EventBus;
using JoyAdmin.Core.Entities.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JoyAdmin.Application.EventBus;

public class TelemetryEventSubscriber : IEventSubscriber,ISingleton
{
    private readonly ILogger<TelemetryEventSubscriber> _logger;
    private readonly IRepository<TelemetryData> _telemetryDataRepository;

    public TelemetryEventSubscriber(ILogger<TelemetryEventSubscriber> logger,   IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _telemetryDataRepository = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IRepository<TelemetryData>>();
    }

    [EventSubscribe("Telemetry:Upload")]
    public async Task UploadTelemetry(EventHandlerExecutingContext context)
    {
        var telemetry = context.Source;
        var payload = telemetry.Payload as JObject;
        if(payload==null) return;
        _logger.LogInformation("上传数据：{Data}", payload.ToString());
        // 从 object 中获取 EntityName, Key, Value
        var entityName = payload["EntityName"]==null?"default":payload["EntityName"].ToString();
        var key = payload["Key"]==null?"default":payload["Key"].ToString();
        
        // 将数据插入到 TelemetryData 表中
        await _telemetryDataRepository.InsertNowAsync(new TelemetryData()
        {
            EntityName = entityName,
            Key =key,
            Value = JsonDocument.Parse(payload.ToString()),
            Time = DateTime.Now
        });
    }
}